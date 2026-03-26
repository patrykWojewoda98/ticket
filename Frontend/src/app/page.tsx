"use client";

import Link from "next/link";
import { useEffect, useState } from "react";
import { Badge } from "@/components/ui/badge";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Loader2 } from "lucide-react";
import { useAuth } from "@/components/common/AuthContext";

type Ticket = {
  id: number;
  userId: number;
  assigneeId?: number | null;
  categoryId?: number | null;
  statusId: number;
  priorityId: number;
  title: string;
  description: string;
};

type TicketStatus = {
  id: number;
  name: string;
};

export default function Home() {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [statuses, setStatuses] = useState<TicketStatus[]>([]); 
  const [isLoading, setIsLoading] = useState(true);

  const { isAuthenticated, user, isLoaded } = useAuth();

  useEffect(() => {
    if (!isLoaded) return;

    if (isAuthenticated && user) {
      
      Promise.all([fetchTickets(), fetchStatuses()]).finally(() => {
        setIsLoading(false);
      });
    } else {
      setIsLoading(false);
      setTickets([]);
    }
  }, [isAuthenticated, user, isLoaded]);

  const fetchStatuses = async () => {
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketStatus`, {
        cache: "no-store",
      });
      if (res.ok) {
        const data = await res.json();
        setStatuses(data);
      }
    } catch (error) {
      console.error("Błąd fetch statusów:", error);
    }
  };

  const fetchTickets = async () => {
    try {
      const timestamp = new Date().getTime();
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket?t=${timestamp}`, {
        cache: "no-store",
      });
      if (!res.ok) throw new Error(`API returned ${res.status}`);
      const data = await res.json();
      setTickets(data);
    } catch (error) {
      console.error("Błąd fetch ticketów:", error);
      setTickets([]);
    }
  };

  const visibleTickets = tickets.filter((ticket) => {
    if (user?.role === "Admin") return true;
    return ticket.userId === user?.id;
  });

  
  const getStatusBadge = (statusId: number) => {
    const statusObj = statuses.find((s) => s.id === statusId);
    const statusName = statusObj ? statusObj.name.toUpperCase() : "NIEZNANY";

   
    let colorClass = "bg-slate-50 border-slate-100 text-slate-600"; 

    if (statusName.includes("OTWARTE") || statusId === 1) {
      colorClass = "bg-emerald-50 border-emerald-100 text-emerald-700";
    } else if (statusName.includes("TOKU") || statusId === 2) {
      colorClass = "bg-amber-50 border-amber-100 text-amber-700";
    } else if (statusName.includes("ZAMKNIĘTE") || statusId === 3) {
      colorClass = "bg-slate-50 border-slate-100 text-slate-600";
    }

    return (
      <Badge variant="outline" className={`font-bold shadow-none px-2 py-0 text-[10px] uppercase ${colorClass}`}>
        {statusName}
      </Badge>
    );
  };

  return (    
    <div className="mx-auto px-8  max-w-7xl font-sans container">
      <header className="mb-12">
       <h1 className="font-semibold text-slate-900 text-3xl tracking-tight mb-12">Witaj {user?.name}</h1>
        <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">{user?.role === "Admin" ? "Wszystkie zgłoszenia" : "Moje zgłoszenia"}</h1>
        <p className="mt-2 text-slate-500 text-base">{user?.role === "Admin" ? "Panel zarządzania wszystkimi zgłoszeniami w systemie." : "Przeglądaj swoje aktywne prośby o pomoc i statusy prac."}</p>
      </header>

      <div className="bg-white shadow-sm border border-slate-200 rounded-lg overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow className="hover:bg-transparent">
              <TableHead className="px-6 w-[30%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Temat</TableHead>
              <TableHead className="px-6 w-[45%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Podgląd treści</TableHead>
              <TableHead className="px-6 w-[15%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Status</TableHead>
              <TableHead className="px-6 w-[10%] h-12 font-bold text-[10px] text-slate-500 text-right uppercase tracking-widest">Opcje</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {isLoading ? (
              <TableRow>
                <TableCell colSpan={4} className="h-40 text-center">
                  <Loader2 className="inline-block w-5 h-5 text-slate-300 animate-spin" />
                </TableCell>
              </TableRow>
            ) : !isAuthenticated ? (
              <TableRow>
                <TableCell colSpan={4} className="h-40 text-slate-400 text-sm text-center italic">
                  Zaloguj się, aby zobaczyć zgłoszenia.
                </TableCell>
              </TableRow>
            ) : visibleTickets.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4} className="h-40 text-slate-400 text-sm text-center">
                  Brak zgłoszeń do wyświetlenia.
                </TableCell>
              </TableRow>
            ) : (
              visibleTickets
                .slice()
                .reverse()
                .map((ticket) => (
                  <TableRow key={ticket.id} className="hover:bg-slate-50/30 border-slate-100 transition-colors">
                    <TableCell className="px-6 py-4 font-medium text-slate-900 truncate">{ticket.title}</TableCell>
                    <TableCell className="px-6 py-4 text-slate-500 text-sm truncate">{ticket.description}</TableCell>
                    <TableCell className="px-6 py-4 text-left">{getStatusBadge(ticket.statusId)}</TableCell>
                    <TableCell className="px-6 py-4 text-right">
                      <Link href={user?.role === "Admin" ? `/admin/tickets/${ticket.id}` : `/customer/tickets/${ticket.id}`} className="font-bold text-indigo-600 hover:text-indigo-900 text-xs transition-colors">
                        SZCZEGÓŁY
                      </Link>
                    </TableCell>
                  </TableRow>
                ))
            )}
          </TableBody>
        </Table>
      </div>
    </div>
  );
}

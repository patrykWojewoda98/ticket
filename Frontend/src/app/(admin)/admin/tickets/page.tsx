"use client";

import React, { useEffect, useState } from "react";
import Link from "next/link";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Loader2, Trash2, Edit3, User } from "lucide-react";
import { useAuth } from "@/components/common/AuthContext";
import { cn } from "@/lib/utils";


interface Ticket {
  id: number;
  priorityId: number;
  title: string;
  userId: number;
  assigneeId?: number | null;
  description: string;
  statusId: number;
  createdAt?: string;
}

interface Status {
  id: number;
  name: string;
}
interface Priority {
  id: number;
  name: string;
}

interface UserData {
  id: number;
  name: string; 
  role: string;
  email: string;
}

export default function AdminTicketsPage() {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [statuses, setStatuses] = useState<Status[]>([]);
  const [priorities, setPriorities] = useState<Priority[]>([]);
  const [users, setUsers] = useState<UserData[]>([]);
  const [loading, setLoading] = useState(true);

  const { isAuthenticated, user, isLoaded } = useAuth();

  const fetchData = async () => {
    if (!isLoaded || !isAuthenticated || user?.role !== "Admin") {
      setLoading(false);
      return;
    }

    try {
      const timestamp = new Date().getTime();
      const [tRes, sRes, pRes, uRes] = await Promise.all([fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket?t=${timestamp}`, { cache: "no-store" }), fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketStatus`), fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketPriority`), fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/User`)]);

      if (!tRes.ok || !sRes.ok || !pRes.ok || !uRes.ok) throw new Error("Błąd pobierania danych");

      setTickets(await tRes.json());
      setStatuses(await sRes.json());
      setPriorities(await pRes.json());
      setUsers(await uRes.json());
    } catch (err: any) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [isLoaded, isAuthenticated, user]);

  const updateTicket = async (ticketId: number, updatedFields: Partial<Ticket>) => {
    const currentTicket = tickets.find((t) => t.id === ticketId);
    if (!currentTicket) return;

    setTickets((prev) => prev.map((t) => (t.id === ticketId ? { ...t, ...updatedFields } : t)));

    try {
      await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket/${ticketId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ ...currentTicket, ...updatedFields }),
      });
    } catch (error) {
      fetchData();
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm("Czy na pewno chcesz usunąć to zgłoszenie?")) return;
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket/${id}`, { method: "DELETE" });
      if (res.ok) setTickets((prev) => prev.filter((t) => t.id !== id));
    } catch (err) {
      alert("Nie udało się usunąć zgłoszenia.");
    }
  };


  const getAssigneeName = (assigneeId?: number | null) => {
    if (!assigneeId) return "Nieprzypisane";

    
    const foundUser = users.find((u) => u.id === assigneeId);

    if (foundUser) {
      
      return foundUser.name || (foundUser as any).Name || "Użytkownik bez imienia";
    }

    return `ID: ${assigneeId}`;
  };

  const getStatusStyles = (statusId: number) => {
    const status = statuses.find((s) => s.id === statusId);
    const name = status?.name.toUpperCase() || "";
    if (name.includes("OTWARTE")) return "text-emerald-700 bg-emerald-50 border-emerald-200";
    if (name.includes("TOKU")) return "text-amber-700 bg-amber-50 border-amber-200";
    return "text-slate-600 bg-slate-100 border-slate-200";
  };

  const getPriorityStyles = (priorityId: number) => {
    const priority = priorities.find((p) => p.id === priorityId);
    const name = priority?.name.toUpperCase() || "";
    if (name.includes("NISKI")) return "text-blue-700 bg-blue-50 border-blue-200";
    if (name.includes("ŚREDNI")) return "text-orange-700 bg-orange-50 border-orange-200";
    if (name.includes("WYSOKI")) return "text-red-700 bg-red-50 border-red-200";
    return "text-slate-600 bg-slate-50 border-slate-200";
  };

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      <header className="flex justify-between items-end mb-12">
        <div>
          <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Panel Administratora</h1>
          <p className="mt-2 text-slate-500 text-base">Zarządzaj zgłoszeniami i przypisaniem pracowników.</p>
        </div>
      </header>

      <div className="bg-white shadow-sm border border-slate-200 rounded-xl overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow>
              <TableHead className="px-6 w-[15%] font-bold text-[10px] text-slate-500 uppercase tracking-widest">Temat</TableHead>
              <TableHead className="px-6 w-[20%] font-bold text-[10px] text-slate-500 uppercase tracking-widest">Przypisany do</TableHead>
              <TableHead className="px-6 w-[18%] font-bold text-[10px] text-slate-500 text-center uppercase tracking-widest">Status</TableHead>
              <TableHead className="px-6 w-[18%] font-bold text-[10px] text-slate-500 text-center uppercase tracking-widest">Priorytet</TableHead>
              <TableHead className="px-6 w-[14%] font-bold text-[10px] text-slate-500 text-right uppercase tracking-widest">Akcja</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={5} className="h-40 text-center">
                  <Loader2 className="inline-block text-slate-300 animate-spin" />
                </TableCell>
              </TableRow>
            ) : (
              tickets
                .slice()
                .reverse()
                .map((ticket) => (
                  <TableRow key={ticket.id} className="hover:bg-slate-50/30 transition-colors">
                    <TableCell className="px-6 py-4 font-medium text-slate-900 truncate">{ticket.title}</TableCell>

                    {/* KOLUMNA: PRZYPISANY DO*/}
                    <TableCell className="px-6 py-4">
                      <div className="flex items-center gap-2">
                        <div className="bg-slate-100 p-1.5 rounded-full">
                          <User className="w-3 h-3 text-slate-500" />
                        </div>
                        <span className="font-medium text-slate-700 text-sm">{getAssigneeName(ticket.assigneeId)}</span>
                      </div>
                    </TableCell>

                    <TableCell className="px-6 py-4 text-center">
                      <Select value={ticket.statusId.toString()} onValueChange={(val) => updateTicket(ticket.id, { statusId: Number(val) })}>
                        <SelectTrigger className={cn("h-8 font-bold text-[10px] uppercase tracking-tight transition-all", getStatusStyles(ticket.statusId))}>
                          <SelectValue />
                        </SelectTrigger>
                        <SelectContent className="bg-white">
                          {statuses.map((s) => (
                            <SelectItem key={s.id} value={s.id.toString()} className="font-bold text-[10px] uppercase">
                              {s.name}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                    </TableCell>

                    <TableCell className="px-6 py-4 text-center">
                      <Select value={ticket.priorityId.toString()} onValueChange={(val) => updateTicket(ticket.id, { priorityId: Number(val) })}>
                        <SelectTrigger className={cn("h-8 font-bold text-[10px] uppercase tracking-tight transition-all", getPriorityStyles(ticket.priorityId))}>
                          <SelectValue />
                        </SelectTrigger>
                        <SelectContent className="bg-white">
                          {priorities.map((p) => (
                            <SelectItem key={p.id} value={p.id.toString()} className="font-bold text-[10px] uppercase">
                              {p.name}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                    </TableCell>

                    <TableCell className="px-6 py-4 text-right">
                      <div className="flex justify-end gap-2">
                        <Link href={`/admin/tickets/${ticket.id}`} className="flex items-center gap-1 bg-blue-600 hover:bg-blue-700 px-3 py-1.5 rounded-lg text-white text-sm transition">
                          <Edit3 className="w-3 h-3" /> Edit
                        </Link>
                        <button onClick={() => handleDelete(ticket.id)} className="hover:bg-red-50 px-2 py-1.5 border border-red-100 rounded-lg text-red-500 transition">
                          <Trash2 className="w-4 h-4" />
                        </button>
                      </div>
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

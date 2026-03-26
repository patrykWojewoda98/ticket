"use client";

import Link from "next/link";
import { useEffect, useState } from "react";
import { Badge } from "@/components/ui/badge";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Loader2, Ticket as TicketIcon } from "lucide-react";
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

export default function AdminHomePage() {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { isAuthenticated, user, isLoaded } = useAuth();

  useEffect(() => {
    if (!isLoaded) return;

    if (isAuthenticated && user?.role === "Admin") {
      fetchTickets();
    } else {
      setIsLoading(false);
      setTickets([]);
    }
  }, [isAuthenticated, user, isLoaded]);

  const fetchTickets = async () => {
    setIsLoading(true);
    try {
      const timestamp = new Date().getTime();
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket?t=${timestamp}`, {
        cache: "no-store",
        headers: {
          "Content-Type": "application/json",
          "Cache-Control": "no-cache, no-store, must-revalidate",
        },
      });

      if (!res.ok) throw new Error(`API returned ${res.status}`);

      const data = await res.json();
      setTickets(data);
    } catch (error) {
      console.error("Błąd fetch:", error);
      setTickets([]);
    } finally {
      setIsLoading(false);
    }
  };

  const getStatusBadge = (statusId: number) => {
    const styles = {
      1: "bg-emerald-50 text-emerald-700 border-emerald-100",
      2: "bg-amber-50 text-amber-700 border-amber-100",
      3: "bg-slate-50 text-slate-600 border-slate-100",
    };
    const labels = { 1: "Otwarte", 2: "W toku", 3: "Zamknięte" };

    return (
      <Badge variant="outline" className={`font-medium shadow-none px-2 py-0 ${styles[statusId as keyof typeof styles] || ""}`}>
        {labels[statusId as keyof typeof labels] || "Nieznany"}
      </Badge>
    );
  };

  const ticketCounts = {
    open: tickets.filter((t) => t.statusId === 1).length,
    pending: tickets.filter((t) => t.statusId === 2).length,
    closed: tickets.filter((t) => t.statusId === 3).length,
  };

  return (
    <div className="mx-auto px-8  max-w-7xl font-sans container">
      <header className="mb-12">
        <h1 className="font-semibold text-slate-900 text-3xl tracking-tight mb-12">Witaj {user?.name}</h1>
        <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Panel Administratora</h1>
        <p className="mt-2 text-slate-500 text-base">Przeglądaj statystyki i zarządzaj wszystkimi zgłoszeniami w systemie.</p>
      </header>

      {/* Sekcja Statystyk */}
      <div className="gap-6 grid grid-cols-1 md:grid-cols-3 mb-12">
        <div className="bg-white shadow-sm p-6 border border-slate-200 rounded-xl">
          <p className="font-medium text-slate-500 text-sm uppercase tracking-wider">Otwarte</p>
          <p className="mt-1 font-bold text-emerald-600 text-3xl">{ticketCounts.open}</p>
        </div>
        <div className="bg-white shadow-sm p-6 border border-slate-200 rounded-xl">
          <p className="font-medium text-slate-500 text-sm uppercase tracking-wider">W toku</p>
          <p className="mt-1 font-bold text-amber-600 text-3xl">{ticketCounts.pending}</p>
        </div>
        <div className="bg-white shadow-sm p-6 border border-slate-200 rounded-xl">
          <p className="font-medium text-slate-500 text-sm uppercase tracking-wider">Zamknięte</p>
          <p className="mt-1 font-bold text-slate-900 text-3xl">{ticketCounts.closed}</p>
        </div>
      </div>
    </div>
  );
}

"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { Badge } from "@/components/ui/badge";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { ArrowLeft, Loader2 } from "lucide-react";
import { cn } from "@/lib/utils";

interface User {
  name: string;
}
interface Status {
  id: number;
  name: string;
}
interface Priority {
  name: string;
}
interface Category {
  name: string;
}

interface TicketDetails {
  user: User | null;
  priority: Priority | null;
  category: Category | null;
}

export default function TicketDetailPage({ params }: { params: any }) {
  const router = useRouter();

  const [isEditing, setIsEditing] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);

  const [ticket, setTicket] = useState<any>(null);
  const [statuses, setStatuses] = useState<Status[]>([]); // Stan na wszystkie statusy
  const [details, setDetails] = useState<TicketDetails>({
    user: null,
    priority: null,
    category: null,
  });
  const [editData, setEditData] = useState({ title: "", description: "" });

  useEffect(() => {
    const fetchData = async () => {
      try {
        setIsLoading(true);
        const resolvedParams = await params;
        const id = resolvedParams.id;

        // 1. Pobierz Ticket i Statusy równolegle
        const [ticketRes, statusesRes] = await Promise.all([fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket/${id}`), fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketStatus`)]);

        if (!ticketRes.ok) return;
        const ticketData = await ticketRes.json();
        const allStatuses = statusesRes.ok ? await statusesRes.json() : [];

        setTicket(ticketData);
        setStatuses(allStatuses);
        setEditData({ title: ticketData.title, description: ticketData.description });

        // 2. Pobierz resztę detali (User, Priority, Category)
        const [u, p, c] = await Promise.all([fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/user/${ticketData.userId}`).then((r) => (r.ok ? r.json() : null)), fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticketpriority/${ticketData.priorityId}`).then((r) => (r.ok ? r.json() : null)), ticketData.categoryId ? fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticketcategory/${ticketData.categoryId}`).then((r) => (r.ok ? r.json() : null)) : null]);

        setDetails({ user: u, priority: p, category: c });
      } catch (error) {
        console.error("Fetch error:", error);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, [params]);

  const handleSave = async () => {
    try {
      setIsSaving(true);
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket/${ticket.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          ...ticket,
          title: editData.title,
          description: editData.description,
        }),
      });

      if (res.ok) {
        setTicket({ ...ticket, title: editData.title, description: editData.description });
        setIsEditing(false);
        router.refresh();
      }
    } catch (error) {
      console.error("Update error:", error);
    } finally {
      setIsSaving(false);
    }
  };

  const getStatusBadge = (statusId: number) => {
    const statusObj = statuses.find((s) => s.id === statusId);
    const statusName = statusObj ? statusObj.name.toUpperCase() : "NIEZNANY";

    let colorClass = "bg-slate-50 border-slate-100 text-slate-600";
    if (statusName.includes("OTWARTE") || statusId === 1) colorClass = "bg-emerald-50 border-emerald-100 text-emerald-700";
    else if (statusName.includes("TOKU") || statusId === 2) colorClass = "bg-amber-50 border-amber-100 text-amber-700";
    else if (statusName.includes("ZAMKNI") || statusId === 3) colorClass = "bg-slate-50 border-slate-100 text-slate-600";

    return (
      <Badge variant="outline" className={cn("shadow-none px-2 py-0 font-bold text-[10px] uppercase", colorClass)}>
        {statusName}
      </Badge>
    );
  };

  if (isLoading)
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <Loader2 className="w-6 h-6 text-slate-300 animate-spin" />
      </div>
    );

  if (!ticket) return <div className="p-12 text-slate-500 text-center italic">Nie znaleziono zgłoszenia.</div>;

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      {/* HEADER */}
      <header className="flex justify-between items-start mb-12">
        <div>
          <div className="flex items-center gap-3 mb-2">
            <Link href="/" className="hover:bg-slate-100 p-2 rounded-full transition-colors">
              <ArrowLeft className="w-4 h-4 text-slate-400" />
            </Link>
            <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Zgłoszenie #{ticket.id}</h1>
          </div>
          <p className="ml-11 text-slate-500 text-base">Zarządzaj szczegółami i treścią zgłoszenia.</p>
        </div>
        <div className="flex gap-4">
          {isEditing ? (
            <>
              <button onClick={() => setIsEditing(false)} className="font-bold text-[10px] text-slate-400 hover:text-slate-600 uppercase tracking-widest transition-colors">
                Anuluj
              </button>
              <button onClick={handleSave} disabled={isSaving} className="bg-slate-900 hover:bg-slate-800 disabled:opacity-50 px-6 py-2 rounded-full font-bold text-[10px] text-white uppercase tracking-widest transition-all">
                {isSaving ? "Zapisywanie..." : "Zapisz zmiany"}
              </button>
            </>
          ) : (
            <button onClick={() => setIsEditing(true)} className="bg-white hover:bg-slate-50 px-6 py-2 border border-slate-200 rounded-full font-bold text-[10px] text-slate-600 uppercase tracking-widest transition-all">
              Edytuj treść
            </button>
          )}
        </div>
      </header>

      <div className="bg-white shadow-sm border border-slate-200 rounded-lg overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow className="hover:bg-transparent">
              <TableHead className="px-6 w-[45%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Temat</TableHead>
              <TableHead className="px-6 w-[25%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Priorytet</TableHead>
              <TableHead className="px-6 w-[15%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Status</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow className="hover:bg-transparent border-none">
              <TableCell className="px-6 h-20 font-medium text-slate-900 align-middle">{isEditing ? <Input value={editData.title} onChange={(e) => setEditData({ ...editData, title: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 h-9 text-sm" /> : <div className="py-2 truncate">{ticket.title}</div>}</TableCell>
              <TableCell className="px-6 h-20 text-slate-600 text-sm align-middle">
                <span className="font-semibold text-slate-500 text-xs uppercase tracking-tight">{details.priority?.name ?? "Brak"}</span>
              </TableCell>
              <TableCell className="px-6 h-20 text-left align-middle">{getStatusBadge(ticket.statusId)}</TableCell>
            </TableRow>
          </TableBody>
        </Table>

        <div className="px-8 py-10 border-slate-100 border-t min-h-[300px]">
          <h2 className="mb-4 font-bold text-[10px] text-slate-400 uppercase tracking-[0.2em]">Opis zgłoszenia</h2>
          {isEditing ? (
            <Textarea value={editData.description} onChange={(e) => setEditData({ ...editData, description: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 min-h-[200px] text-sm leading-relaxed" />
          ) : (
            <div className="py-1">
              <p className="max-w-4xl font-sans text-slate-600 text-sm italic leading-relaxed whitespace-pre-wrap">{ticket.description || "Brak dodatkowego opisu."}</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

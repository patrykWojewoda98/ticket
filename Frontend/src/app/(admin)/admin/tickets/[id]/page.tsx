"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { Badge } from "@/components/ui/badge";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { ArrowLeft, Loader2, UserCheck } from "lucide-react";
import { cn } from "@/lib/utils";



interface Status {
  id: number;
  name: string;
}
interface Priority {
  id: number;
  name: string;
}
interface Category {
  id: number;
  name: string;
}
interface User {
  id: number;
  name: string;
  email?: string;
}

interface Ticket {
  id: number;
  title: string;
  description: string;
  statusId: number;
  priorityId: number;
  categoryId: number;
  userId: number;
  assigneeId?: number | null; 
}

interface EditData {
  title: string;
  description: string;
  statusId: number;
  priorityId: number;
  categoryId: number;
  assigneeId?: number | null;
}

export default function TicketDetailPage({ params }: { params: any }) {
  const router = useRouter();

  const [isEditing, setIsEditing] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);

  const [ticket, setTicket] = useState<Ticket | null>(null);
  const [creator, setCreator] = useState<User | null>(null); 
  const [admins, setAdmins] = useState<User[]>([]); 

  const [statuses, setStatuses] = useState<Status[]>([]);
  const [priorities, setPriorities] = useState<Priority[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);

  const [editData, setEditData] = useState<EditData>({
    title: "",
    description: "",
    statusId: 0,
    priorityId: 0,
    categoryId: 0,
    assigneeId: null,
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        setIsLoading(true);
        const resolvedParams = await params;
        const id = resolvedParams.id;

       
        const [tRes, sRes, pRes, cRes, aRes] = await Promise.all([
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket/${id}`),
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketStatus`),
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketPriority`),
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketCategory`),
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/User/role/Admin`), 
        ]);

        if (!tRes.ok) throw new Error("Failed to fetch ticket");

        const tData: Ticket = await tRes.json();
        setTicket(tData);
        setStatuses(sRes.ok ? await sRes.json() : []);
        setPriorities(pRes.ok ? await pRes.json() : []);
        setCategories(cRes.ok ? await cRes.json() : []);
        setAdmins(aRes.ok ? await aRes.json() : []);

        setEditData({
          title: tData.title,
          description: tData.description,
          statusId: tData.statusId,
          priorityId: tData.priorityId,
          categoryId: tData.categoryId,
          assigneeId: tData.assigneeId,
        });

        
        const uRes = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/user/${tData.userId}`);
        if (uRes.ok) setCreator(await uRes.json());
      } catch (error) {
        console.error("Fetch error:", error);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, [params]);

  const handleSave = async () => {
    if (!ticket) return;
    try {
      setIsSaving(true);
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket/${ticket.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          ...ticket,
          ...editData,
        }),
      });

      if (res.ok) {
        setTicket({ ...ticket, ...editData });
        setIsEditing(false);
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
    if (statusId === 1) colorClass = "bg-emerald-50 border-emerald-100 text-emerald-700";
    else if (statusId === 2) colorClass = "bg-amber-50 border-amber-100 text-amber-700";

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

  if (!ticket) return null;

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      {/* HEADER */}
      <header className="flex justify-between items-start mb-12">
        <div>
          <div className="flex items-center gap-3 mb-2">
            <Link href="/admin/tickets" className="hover:bg-slate-100 p-2 rounded-full transition-colors">
              <ArrowLeft className="w-4 h-4 text-slate-400" />
            </Link>
            <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Zgłoszenie #{ticket.id}</h1>
          </div>
          <p className="ml-11 text-slate-500 text-base">
            Klient potrzebuje pomocy: <span className="font-medium text-slate-900">{creator?.name || "Ładowanie..."}</span>
          </p>
        </div>

        <div className="flex gap-4">
          {isEditing ? (
            <div className="flex items-center gap-4">
              <button onClick={() => setIsEditing(false)} className="font-bold text-[10px] text-slate-400 hover:text-slate-600 uppercase tracking-widest transition-colors">
                Anuluj
              </button>
              <button onClick={handleSave} disabled={isSaving} className="bg-slate-900 hover:bg-slate-800 disabled:opacity-50 shadow-sm px-6 py-2 rounded-full font-bold text-[10px] text-white uppercase tracking-widest transition-all">
                {isSaving ? <Loader2 className="w-3 h-3 animate-spin" /> : "Zapisz zmiany"}
              </button>
            </div>
          ) : (
            <button onClick={() => setIsEditing(true)} className="bg-white hover:bg-slate-50 shadow-sm px-6 py-2 border border-slate-200 rounded-full font-bold text-[10px] text-slate-600 uppercase tracking-widest transition-all">
              Edytuj treść
            </button>
          )}
        </div>
      </header>

      {/* BODY */}
      <div className="bg-white shadow-sm border border-slate-200 rounded-lg overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow className="hover:bg-transparent">
              <TableHead className="px-6 w-[25%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Temat</TableHead>
              <TableHead className="px-6 w-[25%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Osoba Odpowiedzialna</TableHead>
              <TableHead className="px-6 w-[15%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Kategoria</TableHead>
              <TableHead className="px-6 w-[15%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Priorytet</TableHead>
              <TableHead className="px-6 w-[20%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Status</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow className="hover:bg-transparent border-none">
              {/* TYTUŁ */}
              <TableCell className="px-6 h-20 align-middle">{isEditing ? <Input value={editData.title} onChange={(e) => setEditData({ ...editData, title: e.target.value })} className="border-slate-200 rounded-md focus-visible:ring-slate-400 h-9 text-sm" /> : <div className="py-2 font-semibold text-slate-900 truncate">{ticket.title}</div>}</TableCell>

              {/* PRZYPISANIE  */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Select value={editData.assigneeId?.toString() || "0"} onValueChange={(v) => setEditData({ ...editData, assigneeId: v === "0" ? null : Number(v) })}>
                    <SelectTrigger className="border-slate-200 h-9 font-bold text-[10px] uppercase">
                      <SelectValue placeholder="Wybierz..." />
                    </SelectTrigger>
                    <SelectContent>
                      <SelectItem value="0" className="font-bold text-[10px] text-slate-400 italic uppercase">
                        Brak przypisania
                      </SelectItem>
                      {admins.map((adm) => (
                        <SelectItem key={adm.id} value={adm.id.toString()} className="font-bold text-[10px] uppercase">
                          {adm.name}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                ) : (
                  <div className="flex items-center gap-2">
                    <UserCheck className="w-3.5 h-3.5 text-indigo-500" />
                    <span className="font-medium text-slate-700 text-xs">{admins.find((a) => a.id === ticket.assigneeId)?.name || "Nieprzypisane"}</span>
                  </div>
                )}
              </TableCell>

              {/* KATEGORIA */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Select value={editData.categoryId?.toString()} onValueChange={(v) => setEditData({ ...editData, categoryId: Number(v) })}>
                    <SelectTrigger className="border-slate-200 h-9 font-bold text-[10px] uppercase">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent>
                      {categories.map((c) => (
                        <SelectItem key={c.id} value={c.id.toString()} className="font-bold text-[10px] uppercase">
                          {c.name}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                ) : (
                  <span className="font-medium text-slate-500 text-xs uppercase">{categories.find((c) => c.id === ticket.categoryId)?.name || "Brak"}</span>
                )}
              </TableCell>

              {/* PRIORYTET */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Select value={editData.priorityId?.toString()} onValueChange={(v) => setEditData({ ...editData, priorityId: Number(v) })}>
                    <SelectTrigger className="border-slate-200 h-9 font-bold text-[10px] uppercase">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent>
                      {priorities.map((p) => (
                        <SelectItem key={p.id} value={p.id.toString()} className="font-bold text-[10px] uppercase">
                          {p.name}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                ) : (
                  <span className="font-bold text-[10px] text-slate-500 uppercase">{priorities.find((p) => p.id === ticket.priorityId)?.name || "Brak"}</span>
                )}
              </TableCell>

              {/* STATUS */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Select value={editData.statusId?.toString()} onValueChange={(v) => setEditData({ ...editData, statusId: Number(v) })}>
                    <SelectTrigger className="border-slate-200 h-9 font-bold text-[10px] uppercase">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent>
                      {statuses.map((s) => (
                        <SelectItem key={s.id} value={s.id.toString()} className="font-bold text-[10px] uppercase">
                          {s.name}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                ) : (
                  getStatusBadge(ticket.statusId)
                )}
              </TableCell>
            </TableRow>
          </TableBody>
        </Table>

        <div className="px-8 py-10 border-slate-100 border-t min-h-[300px]">
          <h2 className="mb-4 font-bold text-[10px] text-slate-400 uppercase tracking-[0.2em]">Opis zgłoszenia</h2>
          {isEditing ? <Textarea value={editData.description} onChange={(e) => setEditData({ ...editData, description: e.target.value })} className="border-slate-200 rounded-md focus-visible:ring-slate-400 min-h-[200px] text-sm leading-relaxed" /> : <p className="max-w-4xl font-sans text-slate-600 text-sm italic leading-relaxed whitespace-pre-wrap">{ticket.description || "Brak dodatkowego opisu."}</p>}
        </div>
      </div>
    </div>
  );
}

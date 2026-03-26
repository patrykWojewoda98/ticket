"use client";

import React, { useEffect, useState } from "react";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Loader2, Plus, Trash2, Edit3, Check, X } from "lucide-react";
import { useAuth } from "@/components/common/AuthContext";

// --- INTERFEJSY ---
interface DictionaryItem {
  id: number;
  name: string;
}

type DictionaryType = "Status" | "Priority" | "Category";

export default function AdminSettingsPage() {
  const [statuses, setStatuses] = useState<DictionaryItem[]>([]);
  const [priorities, setPriorities] = useState<DictionaryItem[]>([]);
  const [categories, setCategories] = useState<DictionaryItem[]>([]);
  const [loading, setLoading] = useState(true);

  const [activeTab, setActiveTab] = useState<string>("status");
  const [newName, setNewName] = useState("");
  const [isAdding, setIsAdding] = useState(false);

  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingName, setEditingName] = useState("");
  const [isSavingEdit, setIsSavingEdit] = useState(false);

  const { isAuthenticated, user, isLoaded } = useAuth();

  const fetchDictionaries = async () => {
    try {
      const [sRes, pRes, cRes] = await Promise.all([fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketStatus`), fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketPriority`), fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketCategory`)]);

      if (sRes.ok) setStatuses(await sRes.json());
      if (pRes.ok) setPriorities(await pRes.json());
      if (cRes.ok) setCategories(await cRes.json());
    } catch (err) {
      console.error("Błąd fetch:", err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (isLoaded && isAuthenticated && user?.role === "Admin") {
      fetchDictionaries();
    }
  }, [isLoaded, isAuthenticated, user]);

  const handleAddItem = async () => {
    if (!newName.trim()) return;
    setIsAdding(true);
    const typeMap: Record<string, DictionaryType> = {
      status: "Status",
      priority: "Priority",
      category: "Category",
    };
    const type = typeMap[activeTab];

    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket${type}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name: newName }),
      });
      if (res.ok) {
        setNewName("");
        await fetchDictionaries();
      }
    } catch (err) {
      console.error(err);
    } finally {
      setIsAdding(false);
    }
  };

  const handleDeleteItem = async (type: DictionaryType, id: number) => {
    if (!confirm(`Czy na pewno chcesz to usunąć?`)) return;
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket${type}/${id}`, { method: "DELETE" });
      if (res.ok) await fetchDictionaries();
    } catch (err) {
      alert("Błąd podczas usuwania.");
    }
  };

  const handleSaveEdit = async (type: DictionaryType) => {
    if (!editingId || !editingName.trim()) return;
    setIsSavingEdit(true);
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket${type}/${editingId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ [`Ticket${type}Id`]: editingId, name: editingName }),
      });
      if (res.ok) {
        setEditingId(null);
        await fetchDictionaries();
      }
    } catch (err) {
      console.error(err);
    } finally {
      setIsSavingEdit(false);
    }
  };

  if (loading)
    return (
      <div className="flex justify-center items-center h-screen">
        <Loader2 className="w-6 h-6 text-slate-300 animate-spin" />
      </div>
    );

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      <header className="flex justify-between items-end mb-12">
        <div>
          <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Konfiguracja Systemu</h1>
          <p className="mt-2 text-slate-500 text-base">Zarządzaj słownikami kategorii, statusów i priorytetów.</p>
        </div>
      </header>

      <Tabs value={activeTab} onValueChange={setActiveTab} className="w-full">
        <div className="flex justify-between items-center mb-8">
          <TabsList className="bg-slate-100 p-1 border border-slate-200 rounded-xl">
            <TabsTrigger value="status" className="data-[state=active]:bg-white data-[state=active]:shadow-sm px-6 py-2 rounded-lg font-medium text-sm">
              Statusy
            </TabsTrigger>
            <TabsTrigger value="priority" className="data-[state=active]:bg-white data-[state=active]:shadow-sm px-6 py-2 rounded-lg font-medium text-sm">
              Priorytety
            </TabsTrigger>
            <TabsTrigger value="category" className="data-[state=active]:bg-white data-[state=active]:shadow-sm px-6 py-2 rounded-lg font-medium text-sm">
              Kategorie
            </TabsTrigger>
          </TabsList>

          <div className="flex gap-2 w-full max-w-lg">
            <Input placeholder={`Nowa nazwa...`} value={newName} onChange={(e) => setNewName(e.target.value)} className="bg-white shadow-none border-slate-200 focus-visible:ring-indigo-500" />
            <button disabled={isAdding || !newName} onClick={handleAddItem} className="flex items-center gap-2 bg-slate-900 hover:bg-slate-800 shadow-sm px-6 py-2.5 rounded-full font-bold text-[10px] text-white uppercase tracking-widest active:scale-95 transition-all">
              {isAdding ? <Loader2 className="w-4 h-4 animate-spin" /> : <Plus className="mr-2 w-4 h-4" />}
              Dodaj
            </button>
          </div>
        </div>

        {["status", "priority", "category"].map((tab) => (
          <TabsContent key={tab} value={tab}>
            <DictionaryTable items={tab === "status" ? statuses : tab === "priority" ? priorities : categories} type={(tab.charAt(0).toUpperCase() + tab.slice(1)) as DictionaryType} onDelete={handleDeleteItem} editingId={editingId} editingName={editingName} setEditingName={setEditingName} setEditingId={setEditingId} onSave={handleSaveEdit} isSaving={isSavingEdit} />
          </TabsContent>
        ))}
      </Tabs>
    </div>
  );
}

function DictionaryTable({ items, type, onDelete, editingId, editingName, setEditingName, setEditingId, onSave, isSaving }: any) {
  return (
    <div className="bg-white shadow-sm border border-slate-200 rounded-xl overflow-hidden">
      <Table className="w-full table-fixed">
        <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
          <TableRow>
            <TableHead className="px-6 w-[65%] font-bold text-[10px] text-slate-500 uppercase tracking-widest">Nazwa</TableHead>
            <TableHead className="px-6 w-[20%] font-bold text-[10px] text-slate-500 text-right uppercase tracking-widest">Akcja</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {items.map((item: any) => {
            const isEditing = editingId === item.id;
            return (
              <TableRow key={item.id} className="hover:bg-slate-50/30 border-slate-50 last:border-0 border-b h-16 transition-colors">
                <TableCell className="px-6 py-4">{isEditing ? <Input value={editingName} onChange={(e) => setEditingName(e.target.value)} className="border-slate-200 focus-visible:ring-indigo-500 h-8 font-semibold text-sm" /> : <div className="font-semibold text-slate-900 truncate">{item.name}</div>}</TableCell>
                <TableCell className="px-6 py-4 text-right">
                  <div className="flex justify-end gap-2">
                    {isEditing ? (
                      <>
                        <button onClick={() => onSave(type)} disabled={isSaving} className="flex items-center gap-1 bg-emerald-600 hover:bg-emerald-700 px-3 py-1.5 rounded-lg text-white text-xs active:scale-95 transition">
                          {isSaving ? <Loader2 className="w-3 h-3 animate-spin" /> : <Check className="w-3 h-3" />} Save
                        </button>
                        <button onClick={() => setEditingId(null)} className="hover:bg-slate-100 px-2 py-1.5 border border-slate-200 rounded-lg text-slate-500 transition">
                          <X className="w-4 h-4" />
                        </button>
                      </>
                    ) : (
                      <>
                        <button
                          onClick={() => {
                            setEditingId(item.id);
                            setEditingName(item.name);
                          }}
                          className="flex items-center gap-1 bg-blue-600 hover:bg-blue-700 px-3 py-1.5 rounded-lg text-white text-sm transition"
                        >
                          <Edit3 className="w-3 h-3" /> Edit
                        </button>
                        <button onClick={() => onDelete(type, item.id)} className="hover:bg-red-50 px-2 py-1.5 border border-red-100 rounded-lg text-red-500 transition">
                          <Trash2 className="w-4 h-4" />
                        </button>
                      </>
                    )}
                  </div>
                </TableCell>
              </TableRow>
            );
          })}
        </TableBody>
      </Table>
    </div>
  );
}

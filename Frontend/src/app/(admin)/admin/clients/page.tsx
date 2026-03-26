"use client";

import React, { useEffect, useState, useMemo } from "react";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Edit3, Loader2, Plus, Trash2, UserPlus } from "lucide-react";
import { cn } from "@/lib/utils";
import Link from "next/link";

// Zakładając, że te typy są importowane lub zdefiniowane lokalnie
export interface User {
  id: number;
  name: string;
  email: string;
  companyId: number;
}

export interface Company {
  id: number;
  name: string;
}

const API_USERS = `${process.env.NEXT_PUBLIC_APP_URL}/api/user`;
const API_COMPANIES = `${process.env.NEXT_PUBLIC_APP_URL}/api/company`;

export default function ClientsPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [companies, setCompanies] = useState<Company[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchData = async () => {
    setLoading(true);
    setError(null);
    try {
      const [usersRes, companiesRes] = await Promise.all([fetch(API_USERS, { cache: "no-store" }), fetch(API_COMPANIES, { cache: "no-store" })]);

      if (!usersRes.ok || !companiesRes.ok) throw new Error("Błąd pobierania danych");

      const usersData = await usersRes.json();
      const companiesData = await companiesRes.json();

      setUsers(usersData);
      setCompanies(companiesData);
    } catch (err: any) {
      setError(err.message || "Nieznany błąd");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const companyMap = useMemo(() => {
    return Object.fromEntries(companies.map((c) => [c.id, c.name]));
  }, [companies]);

  const handleDelete = async (id: number) => {
    if (!confirm("Na pewno usunąć użytkownika?")) return;

    try {
      const res = await fetch(`${API_USERS}/${id}`, { method: "DELETE" });
      if (!res.ok) throw new Error("Błąd usuwania");
      setUsers((prev) => prev.filter((u) => u.id !== id));
    } catch (err) {
      console.error(err);
      alert("Nie udało się usunąć użytkownika");
    }
  };

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      <header className="flex justify-between items-end mb-12">
        <div>
          <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Klienci</h1>
          <p className="mt-2 text-slate-500 text-base">Zarządzaj bazą użytkowników i ich powiązaniami z firmami.</p>
        </div>
        {/* Przycisk akcji w Twoim stylu */}{" "}
        <Link href="/admin/clients/new" className="flex items-center gap-2 bg-slate-900 hover:bg-slate-800 shadow-sm px-6 py-2.5 rounded-full font-bold text-[10px] text-white uppercase tracking-widest active:scale-95 transition-all">
          <Plus className="w-3.5 h-3.5" /> Dodaj klienta
        </Link>
      </header>

      <div className="bg-white shadow-sm border border-slate-200 rounded-lg overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow>
              <TableHead className="px-6 w-[30%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Imię i Nazwisko</TableHead>
              <TableHead className="px-6 w-[30%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Email</TableHead>
              <TableHead className="px-6 w-[32%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Firma</TableHead>
              <TableHead className="px-6 w-[14%] h-12 font-bold text-[10px] text-slate-500 text-right uppercase tracking-widest">Akcja</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={4} className="h-40 text-center">
                  <Loader2 className="inline-block w-6 h-6 text-slate-300 animate-spin" />
                </TableCell>
              </TableRow>
            ) : error ? (
              <TableRow>
                <TableCell colSpan={4} className="h-40 font-medium text-red-500 text-sm text-center">
                  {error}
                </TableCell>
              </TableRow>
            ) : (
              users.map((user) => (
                <TableRow key={user.id} className="hover:bg-slate-50/30 border-slate-100 transition-colors">
                  <TableCell className="px-6 py-4 font-medium text-slate-900 truncate">{user.name}</TableCell>
                  <TableCell className="px-6 py-4 text-slate-500 text-sm truncate">{user.email}</TableCell>
                  <TableCell className="px-6 py-4">
                    <span className="font-semibold text-[10px] text-slate-500 uppercase tracking-tight">{companyMap[user.companyId] || "Brak firmy"}</span>
                  </TableCell>
                  <TableCell className="flex gap-2 px-6 py-4 text-right">
                    <Link href={`/admin/clients/${user.id}/edit`} className="flex items-center gap-1 bg-blue-600 hover:bg-blue-700 px-3 py-1.5 rounded-lg text-white text-sm transition">
                      <Edit3 className="w-3 h-3" /> Edit
                    </Link>
                    <button onClick={() => handleDelete(user.id)} className="hover:bg-red-50 px-2 py-1.5 border border-red-200 rounded-lg text-red-600 transition">
                      <Trash2 className="w-4 h-4" />
                    </button>
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </div>

      {!loading && users.length === 0 && !error && <p className="mt-8 text-slate-400 text-sm text-center italic">Brak zarejestrowanych klientów.</p>}
    </div>
  );
}

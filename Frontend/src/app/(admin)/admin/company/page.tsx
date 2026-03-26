"use client";

import React, { useEffect, useState } from "react";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Edit3, Loader2, Trash2, Plus } from "lucide-react";
import Link from "next/link";


export interface Company {
  id: number;
  name: string;
  email?: string;
  phone?: string;
}

const API_COMPANIES = `${process.env.NEXT_PUBLIC_APP_URL}/api/company`;

export default function CompaniesPage() {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchCompanies = async () => {
    setLoading(true);
    setError(null);
    try {
      const res = await fetch(API_COMPANIES, { cache: "no-store" });
      if (!res.ok) throw new Error("Błąd pobierania firm");
      const data = await res.json();
      setCompanies(data);
    } catch (err: any) {
      setError(err.message || "Nieznany błąd");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCompanies();
  }, []);

  const handleDelete = async (id: number) => {
    if (!confirm("Czy na pewno chcesz usunąć tę firmę? Spowoduje to odpięcie jej od wszystkich użytkowników.")) return;

    try {
      const res = await fetch(`${API_COMPANIES}/${id}`, {
        method: "DELETE",
      });

      if (!res.ok) throw new Error("Nie udało się usunąć firmy (może mieć przypisanych użytkowników)");

      setCompanies((prev) => prev.filter((c) => c.id !== id));
    } catch (err: any) {
      console.error(err);
      alert(err.message || "Błąd podczas usuwania");
    }
  };

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      <header className="flex justify-between items-end mb-12">
        <div>
          <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Firmy</h1>
          <p className="mt-2 text-slate-500 text-base">Zarządzaj listą przedsiębiorstw, ich danymi kontaktowymi i organizacją.</p>
        </div>

        <Link href="/admin/company/new" className="flex items-center gap-2 bg-slate-900 hover:bg-slate-800 shadow-sm px-6 py-2.5 rounded-full font-bold text-[10px] text-white uppercase tracking-widest active:scale-95 transition-all">
          <Plus className="w-3.5 h-3.5" /> Dodaj firmę
        </Link>
      </header>

      <div className="bg-white shadow-sm border border-slate-200 rounded-lg overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow>
              <TableHead className="px-6 w-[25%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Nazwa Firmy</TableHead>
              <TableHead className="px-6 w-[25%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Email</TableHead>
              <TableHead className="px-6 w-[15%] h-12 font-bold text-[10px] text-slate-500 text-right uppercase tracking-widest">Akcja</TableHead>
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
            ) : companies.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4} className="h-40 text-slate-400 text-sm text-center italic">
                  Brak zarejestrowanych firm.
                </TableCell>
              </TableRow>
            ) : (
              companies.map((company) => (
                <TableRow key={company.id} className="hover:bg-slate-50/30 border-slate-100 transition-colors">
                  <TableCell className="px-6 py-4 font-medium text-slate-900 truncate">{company.name}</TableCell>
                  <TableCell className="px-6 py-4 text-slate-500 text-sm truncate">{company.email || "—"}</TableCell>
                  <TableCell className="px-6 py-4">
                    <div className="flex justify-end gap-2">
                      <Link href={`/admin/company/${company.id}/edit`} className="flex items-center gap-1 bg-blue-600 hover:bg-blue-700 px-3 py-1.5 rounded-lg text-white text-sm transition">
                        <Edit3 className="w-3 h-3" /> Edit
                      </Link>
                      <button onClick={() => handleDelete(company.id)} className="hover:bg-red-50 px-2 py-1.5 border border-red-200 rounded-lg text-red-600 active:scale-95 transition">
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

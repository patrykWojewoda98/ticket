"use client";

import React, { useEffect, useState } from "react";
import Link from "next/link";
import { useRouter, useParams } from "next/navigation";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Input } from "@/components/ui/input";
import { ArrowLeft, Loader2 } from "lucide-react";


interface Company {
  id: number;
  name: string;
  email: string;
  phoneNumber: string; 
  address: string; 
}

const API = `${process.env.NEXT_PUBLIC_APP_URL}/api/company`;

export default function EditCompanyPage() {
  const router = useRouter();
  const params = useParams();
  const companyId = Number(params.id);

  const [company, setCompany] = useState<Company | null>(null);
  const [isEditing, setIsEditing] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [formData, setFormData] = useState({
    name: "",
    email: "",
    phoneNumber: "",
    address: "",
  });

  useEffect(() => {
    const fetchCompany = async () => {
      try {
        setIsLoading(true);
        const res = await fetch(`${API}/${companyId}`, { cache: "no-store" });
        if (!res.ok) throw new Error("Nie znaleziono firmy");

        const data = await res.json();
        setCompany(data);
        setFormData({
          name: data.name ?? "",
          email: data.email ?? "",
          phoneNumber: data.phoneNumber ?? "",
          address: data.address ?? "",
        });
      } catch (err: any) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };
    fetchCompany();
  }, [companyId]);

  const handleSave = async () => {
    try {
      setIsSaving(true);
      const res = await fetch(`${API}/${companyId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          CompanyId: companyId,
          Name: formData.name,
          Email: formData.email,
          PhoneNumber: formData.phoneNumber, 
          Address: formData.address,
        }),
      });

      if (!res.ok) throw new Error("Błąd podczas zapisywania zmian");

      setCompany({ ...company!, ...formData });
      setIsEditing(false);
      router.refresh();
    } catch (err: any) {
      alert(err.message);
    } finally {
      setIsSaving(false);
    }
  };

  if (isLoading)
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <Loader2 className="w-6 h-6 text-slate-300 animate-spin" />
      </div>
    );

  if (error || !company) return <div className="p-12 text-slate-500 text-center italic">Firma nie została znaleziona.</div>;

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      <header className="flex justify-between items-start mb-12">
        <div>
          <div className="flex items-center gap-3 mb-2">
            <Link href="/admin/company" className="hover:bg-slate-100 p-2 rounded-full transition-colors">
              <ArrowLeft className="w-4 h-4 text-slate-400" />
            </Link>
            <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Profil Firmy #{company.id}</h1>
          </div>
          <p className="ml-11 text-slate-500 text-base">Zarządzaj danymi kontaktowymi i informacjami o przedsiębiorstwie.</p>
        </div>
        <div className="flex gap-4">
          {isEditing ? (
            <>
              <button onClick={() => setIsEditing(false)} className="font-bold text-[10px] text-slate-400 hover:text-slate-600 uppercase tracking-widest transition-colors">
                Anuluj
              </button>
              <button onClick={handleSave} disabled={isSaving} className="bg-slate-900 hover:bg-slate-800 disabled:opacity-50 px-6 py-2 rounded-full font-bold text-[10px] text-white uppercase tracking-widest transition-all">
                {isSaving ? "Zapisywanie..." : "Zapisz dane"}
              </button>
            </>
          ) : (
            <button onClick={() => setIsEditing(true)} className="bg-white hover:bg-slate-50 px-6 py-2 border border-slate-200 rounded-full font-bold text-[10px] text-slate-600 uppercase tracking-widest transition-all">
              Edytuj profil
            </button>
          )}
        </div>
      </header>

      <div className="bg-white shadow-sm border border-slate-200 rounded-lg overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow className="hover:bg-transparent">
              <TableHead className="px-6 w-[25%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Nazwa Firmy</TableHead>
              <TableHead className="px-6 w-[25%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Email</TableHead>
              <TableHead className="px-6 w-[20%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Telefon</TableHead>
              <TableHead className="px-6 w-[30%] h-12 font-bold text-[10px] text-slate-500 uppercase tracking-widest">Adres</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow className="hover:bg-transparent border-none">
              {/* KOLUMNA: NAZWA */}
              <TableCell className="px-6 h-20 align-middle">{isEditing ? <Input value={formData.name} onChange={(e) => setFormData({ ...formData, name: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 h-9 text-sm" /> : <div className="font-semibold text-slate-900 truncate uppercase tracking-tight">{company.name}</div>}</TableCell>

              {/* KOLUMNA: EMAIL */}
              <TableCell className="px-6 h-20 align-middle">{isEditing ? <Input value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 h-9 text-sm" /> : <div className="text-slate-600 text-sm truncate">{company.email || "—"}</div>}</TableCell>

              {/* KOLUMNA: TELEFON */}
              <TableCell className="px-6 h-20 align-middle">{isEditing ? <Input value={formData.phoneNumber} onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 h-9 text-sm" /> : <div className="text-slate-600 text-sm truncate">{company.phoneNumber || "—"}</div>}</TableCell>

              {/* KOLUMNA: ADRES */}
              <TableCell className="px-6 h-20 align-middle">{isEditing ? <Input value={formData.address} onChange={(e) => setFormData({ ...formData, address: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 h-9 text-sm" /> : <div className="text-slate-600 text-sm truncate">{company.address || "—"}</div>}</TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </div>
    </div>
  );
}

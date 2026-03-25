"use client";

import React, { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { Loader2, UserPlus, ArrowLeft } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import Link from "next/link";

export default function CreateClientPage() {
  const router = useRouter();

  const [form, setForm] = useState({
    name: "",
    email: "",
    companyId: "",
    role: "user",
    password: "",
  });

  const [companies, setCompanies] = useState<{ id: number; name: string }[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCompanies = async () => {
      try {
        const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Company`);
        if (!res.ok) throw new Error("Nie udało się pobrać listy firm");
        const data = await res.json();
        setCompanies(data);
      } catch (err) {
        console.error(err);
      }
    };
    fetchCompanies();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    if (error) setError(null);
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    if (form.password.length < 6) {
      setError("Hasło musi mieć minimum 6 znaków");
      setLoading(false);
      return;
    }

    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/User`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          CompanyId: form.companyId ? Number(form.companyId) : null,
          Email: form.email,
          Role: form.role === "admin" ? "Admin" : "User",
          Name: form.name,
          Password: form.password,
        }),
      });

      if (!res.ok) {
        const text = await res.text();
        throw new Error(text || "Wystąpił błąd podczas tworzenia użytkownika");
      }

      router.push("/admin/clients");
      router.refresh();
    } catch (err: any) {
      setError(err.message);
      setLoading(false);
    }
  };

  return (
    <div className="mx-auto px-6 py-12 max-w-2xl container">
      {/* HEADER */}

      <header className="mb-10 text-center">
        <h1 className="font-bold text-slate-900 text-2xl uppercase tracking-tighter">Dodaj nowego klienta</h1>
        <p className="text-slate-500 text-sm">Utwórz konto dla nowego użytkownika i przypisz go do odpowiedniej firmy.</p>
      </header>

      <form onSubmit={handleSubmit} className="space-y-6">
        {/* BŁĄD */}
        {error && <div className="bg-red-50 slide-in-from-top-1 p-4 border border-red-100 rounded-xl font-bold text-[11px] text-red-600 text-center uppercase tracking-widest animate-in fade-in">⚠️ {error}</div>}

        {/* IMIĘ I NAZWISKO */}
        <div className="space-y-2">
          <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Imię i Nazwisko *</Label>
          <Input name="name" placeholder="Jan Kowalski" value={form.name} onChange={handleChange} required className="shadow-none border-slate-200 rounded-xl focus:ring-2 focus:ring-slate-900 h-11 font-medium" />
        </div>

        {/* EMAIL I HASŁO */}
        <div className="gap-6 grid grid-cols-1 md:grid-cols-2">
          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Adres Email *</Label>
            <Input type="email" name="email" placeholder="przyklad@firma.pl" value={form.email} onChange={handleChange} required className="shadow-none border-slate-200 rounded-xl focus:ring-2 focus:ring-slate-900 h-11 font-medium" />
          </div>
          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Hasło *</Label>
            <Input type="password" name="password" placeholder="••••••••" value={form.password} onChange={handleChange} required className="shadow-none border-slate-200 rounded-xl focus:ring-2 focus:ring-slate-900 h-11 font-medium" />
          </div>
        </div>

        {/* FIRMA I ROLA */}
        <div className="gap-6 grid grid-cols-1 md:grid-cols-2">
          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Firma</Label>
            <select name="companyId" value={form.companyId} onChange={handleChange} className="flex bg-white px-4 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-slate-900 w-full h-11 font-medium text-sm transition-all appearance-none cursor-pointer">
              <option value="">Wybierz firmę...</option>
              {companies.map((company) => (
                <option key={company.id} value={company.id}>
                  {company.name}
                </option>
              ))}
            </select>
          </div>

          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Rola *</Label>
            <select name="role" value={form.role} onChange={handleChange} className="flex bg-white px-4 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-slate-900 w-full h-11 font-medium text-sm transition-all appearance-none cursor-pointer">
              <option value="user">User</option>
              <option value="admin">Admin</option>
            </select>
          </div>
        </div>

        {/* ACTIONS */}
        <div className="flex justify-end pt-6">
          <Button type="submit" disabled={loading} className="bg-slate-900 shadow-slate-100 shadow-xl hover:shadow-slate-200 px-10 rounded-full h-12 font-bold text-[11px] text-white uppercase tracking-widest hover:scale-[0.98] transition-all">
            {loading ? (
              <Loader2 className="w-4 h-4 animate-spin" />
            ) : (
              <>
                <UserPlus className="mr-2 w-4 h-4" /> Zapisz użytkownika
              </>
            )}
          </Button>
        </div>
      </form>
    </div>
  );
}

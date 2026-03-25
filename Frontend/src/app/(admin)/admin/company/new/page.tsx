"use client";

import React, { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { Loader2, Building2, ArrowLeft } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import Link from "next/link";

export default function CreateCompanyPage() {
  const router = useRouter();

  const [form, setForm] = useState({
    name: "",
    email: "",
    phoneNumber: "",
    address: "",
    userId: "", // Dodane pole UserId
  });

  const [users, setUsers] = useState<{ id: number; name: string }[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Pobieramy użytkowników, bo backend wymaga UserId
  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/User`);
        if (res.ok) {
          const data = await res.json();
          setUsers(data);
        }
      } catch (err) {
        console.error("Nie udało się pobrać użytkowników", err);
      }
    };
    fetchUsers();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    if (error) setError(null);
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    // Oczyszczanie numeru telefonu (częsty wymóg walidatorów: brak spacji)
    const cleanPhone = form.phoneNumber.replace(/\s/g, "");

    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Company`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          Name: form.name,
          Email: form.email,
          PhoneNumber: cleanPhone, // Wysłanie oczyszczonego numeru
          Address: form.address,
          UserId: Number(form.userId), // Konwersja na int dla C#
        }),
      });

      if (!res.ok) {
        const errorData = await res.json();
        // Wyciągamy błędy z FluentValidation, jeśli API je zwraca w JSONie
        throw new Error(errorData.detail || "Błąd walidacji danych. Sprawdź UserId i Telefon.");
      }

      router.push("/admin/company");
      router.refresh();
    } catch (err: any) {
      setError(err.message);
      setLoading(false);
    }
  };

  return (
    <div className="mx-auto px-6 py-12 max-w-2xl container">
      <header className="mb-10 text-center">
        <h1 className="font-bold text-slate-900 text-2xl uppercase tracking-tighter">Dodaj nową firmę</h1>
        <p className="text-slate-500 text-sm">Uzupełnij dane, aby zarejestrować podmiot w systemie.</p>
      </header>

      <form onSubmit={handleSubmit} className="space-y-6">
        {error && <div className="bg-red-50 p-4 border border-red-100 rounded-xl font-bold text-[11px] text-red-600 text-center uppercase tracking-widest">⚠️ {error}</div>}

        {/* UŻYTKOWNIK (WYMAGANY PRZEZ BACKEND) */}
        <div className="space-y-2">
          <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Przypisany Użytkownik (Właściciel) *</Label>
          <select name="userId" value={form.userId} onChange={handleChange} required className="flex bg-white px-4 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-slate-900 w-full h-11 font-medium text-sm transition-all appearance-none cursor-pointer">
            <option value="">Wybierz użytkownika...</option>
            {users.map((u) => (
              <option key={u.id} value={u.id}>
                {u.name} (#{u.id})
              </option>
            ))}
          </select>
        </div>

        <div className="space-y-2">
          <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Nazwa Firmy *</Label>
          <Input name="name" placeholder="Acme Sp. z o.o." value={form.name} onChange={handleChange} required className="shadow-none border-slate-200 rounded-xl h-11" />
        </div>

        <div className="gap-6 grid grid-cols-1 md:grid-cols-2">
          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Email</Label>
            <Input type="email" name="email" placeholder="biuro@firma.pl" value={form.email} onChange={handleChange} className="shadow-none border-slate-200 rounded-xl h-11" />
          </div>
          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Telefon (np. +48123456789) *</Label>
            <Input name="phoneNumber" placeholder="+48123456789" value={form.phoneNumber} onChange={handleChange} required className="shadow-none border-slate-200 rounded-xl h-11" />
          </div>
        </div>

        <div className="space-y-2">
          <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Adres</Label>
          <Input name="address" placeholder="ul. Sezamkowa 1, Warszawa" value={form.address} onChange={handleChange} className="shadow-none border-slate-200 rounded-xl h-11" />
        </div>

        <div className="flex justify-end pt-6">
          <Button type="submit" disabled={loading} className="bg-slate-900 px-10 rounded-full h-12 font-bold text-[11px] text-white uppercase tracking-widest transition-all">
            {loading ? (
              <Loader2 className="w-4 h-4 animate-spin" />
            ) : (
              <>
                <Building2 className="mr-2 w-4 h-4" /> Zapisz firmę
              </>
            )}
          </Button>
        </div>
      </form>
    </div>
  );
}

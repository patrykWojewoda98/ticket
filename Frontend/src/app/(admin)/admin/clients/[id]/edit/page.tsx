"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { useRouter, useParams } from "next/navigation";
import { Badge } from "@/components/ui/badge";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Input } from "@/components/ui/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { ArrowLeft, Loader2 } from "lucide-react";
import { cn } from "@/lib/utils";

interface User {
  id: number;
  companyId: number;
  name: string;
  email: string;
  role: string;
}

interface Company {
  id: number;
  name: string;
}

const API = `${process.env.NEXT_PUBLIC_APP_URL}/api`;

export default function EditClientPage() {
  const router = useRouter();
  const params = useParams();
  const userId = Number(params.id);

  const [user, setUser] = useState<User | null>(null);
  const [companies, setCompanies] = useState<Company[]>([]);
  const [isEditing, setIsEditing] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [formData, setFormData] = useState({
    companyId: 0,
    name: "",
    email: "",
    role: "user",
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        setIsLoading(true);
        const [userRes, companiesRes] = await Promise.all([fetch(`${API}/user/${userId}`), fetch(`${API}/company`)]);

        if (!userRes.ok) throw new Error("Błąd pobierania użytkownika");
        const userData = await userRes.json();
        const companiesData = await companiesRes.json();

        setUser(userData);
        setCompanies(companiesData);
        setFormData({
          companyId: userData.companyId ?? 0,
          name: userData.name ?? "",
          email: userData.email ?? "",
          role: userData.role?.toLowerCase() ?? "user",
        });
      } catch (err: any) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, [userId]);

  const handleSave = async () => {
    try {
      setIsSaving(true);
      const payload = {
        UserId: userId,
        Name: formData.name,
        CompanyId: formData.companyId,
        Email: formData.email,
        Role: formData.role === "admin" ? "Admin" : "User",
      };

      const res = await fetch(`${API}/user/${userId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (!res.ok) throw new Error("Błąd zapisu");

      setUser({ ...user!, ...formData, role: payload.Role });
      setIsEditing(false);
      router.refresh();
    } catch (err: any) {
      alert(err.message);
    } finally {
      setIsSaving(false);
    }
  };

  const getRoleBadge = (role: string) => {
    const isAdmin = role.toLowerCase() === "admin";
    return (
      <Badge variant="outline" className={cn("shadow-none px-2 py-0 font-bold text-[10px] uppercase", isAdmin ? "bg-indigo-50 border-indigo-100 text-indigo-700" : "bg-slate-50 border-slate-100 text-slate-600")}>
        {role}
      </Badge>
    );
  };

  if (isLoading)
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <Loader2 className="w-6 h-6 text-slate-300 animate-spin" />
      </div>
    );

  if (error || !user) return <div className="p-12 text-slate-500 text-center italic">Użytkownik nie został znaleziony.</div>;

  return (
    <div className="mx-auto px-8 py-16 max-w-7xl font-sans container">
      <header className="flex justify-between items-start mb-12">
        <div>
          <div className="flex items-center gap-3 mb-2">
            <Link href="/admin/clients" className="hover:bg-slate-100 p-2 rounded-full transition-colors">
              <ArrowLeft className="w-4 h-4 text-slate-400" />
            </Link>
            <h1 className="font-semibold text-slate-900 text-3xl tracking-tight">Profil Klienta #{user.id}</h1>
          </div>
          <p className="ml-11 text-slate-500 text-base">Zarządzaj danymi osobowymi i uprawnieniami użytkownika.</p>
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
              Edytuj profil
            </button>
          )}
        </div>
      </header>

      <div className="bg-white shadow-sm border border-slate-200 rounded-lg overflow-hidden">
        <Table className="w-full table-fixed">
          <TableHeader className="bg-slate-50/50 border-slate-100 border-b">
            <TableRow className="hover:bg-transparent">
              <TableHead className="px-6 w-[30%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Imię i Nazwisko</TableHead>
              <TableHead className="px-6 w-[35%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Adres Email</TableHead>
              <TableHead className="px-6 w-[15%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Rola</TableHead>
              <TableHead className="px-6 w-[20%] h-12 font-bold text-[10px] text-slate-500 text-left uppercase tracking-widest">Firma</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow className="hover:bg-transparent border-none">
              {/* KOLUMNA: IMIĘ */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Input value={formData.name} onChange={(e) => setFormData({ ...formData, name: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 h-9 text-sm" />
                ) : (
                  <div className="font-medium text-slate-900 truncate" title={user.name}>
                    {user.name}
                  </div>
                )}
              </TableCell>

              {/* KOLUMNA: EMAIL  */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Input value={formData.email} onChange={(e) => setFormData({ ...formData, email: e.target.value })} className="shadow-none border-slate-200 focus-visible:ring-indigo-500 h-9 text-sm" />
                ) : (
                  <div className="text-slate-600 text-sm truncate" title={user.email}>
                    {user.email}
                  </div>
                )}
              </TableCell>

              {/* KOLUMNA: ROLA */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Select value={formData.role} onValueChange={(val) => setFormData({ ...formData, role: val })}>
                    <SelectTrigger className="shadow-none border-slate-200 h-9 font-bold text-[10px] uppercase">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent>
                      <SelectItem value="user" className="font-bold text-[10px] uppercase">
                        User
                      </SelectItem>
                      <SelectItem value="admin" className="font-bold text-[10px] uppercase">
                        Admin
                      </SelectItem>
                    </SelectContent>
                  </Select>
                ) : (
                  getRoleBadge(user.role)
                )}
              </TableCell>

              {/* KOLUMNA: FIRMA  */}
              <TableCell className="px-6 h-20 align-middle">
                {isEditing ? (
                  <Select value={formData.companyId.toString()} onValueChange={(val) => setFormData({ ...formData, companyId: Number(val) })}>
                    <SelectTrigger className="shadow-none border-slate-200 h-9 font-bold text-[10px] uppercase">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent>
                      {companies.map((c) => (
                        <SelectItem key={c.id} value={c.id.toString()} className="font-bold text-[10px] uppercase">
                          {c.name}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                ) : (
                  <div className="font-semibold text-slate-500 text-xs truncate uppercase tracking-tight">{companies.find((c) => c.id === user.companyId)?.name ?? "Brak"}</div>
                )}
              </TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </div>
    </div>
  );
}

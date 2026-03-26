"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { Loader2, Send } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";

export default function NewTicketPage() {
  const router = useRouter();
  const [form, setForm] = useState({
    assigneeId: "",
    categoryId: "",
    title: "",
    description: "",
  });
  const [statuses, setStatuses] = useState<any[]>([]); // Nowy stan
  const [categories, setCategories] = useState<any[]>([]);
  const [assignees, setAssignees] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [catRes, userRes, statRes] = await Promise.all([
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketCategory`),
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/User`),
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketStatus`), // Zakładam taką ścieżkę API
        ]);
        if (catRes.ok) setCategories(await catRes.json());
        if (statRes.ok) setStatuses(await statRes.json()); // Zapisujemy statusy
        if (userRes.ok) {
          const users = await userRes.json();
          const admins = users.filter((u: any) => u.role === "Admin" || u.isAdmin === true);
          setAssignees(admins);
        }
      } catch (err) {
        console.error("Błąd pobierania danych:", err);
      }
    };
    fetchData();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    if (error) setError(null);
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    if (form.title.trim().length < 5) {
      setError("Tytuł musi mieć minimum 5 znaków");
      setLoading(false);
      return;
    }

    if (form.description.trim().length < 10) {
      setError("Opis musi mieć minimum 10 znaków");
      setLoading(false);
      return;
    }

    if (!form.categoryId) {
      setError("Proszę wybrać kategorię");
      setLoading(false);
      return;
    }

    const userData = typeof window !== "undefined" ? localStorage.getItem("user") : null;
    const user = userData ? JSON.parse(userData) : null;

    if (!user?.id) {
      setError("Sesja wygasła. Zaloguj się ponownie.");
      setLoading(false);
      return;
    }
    const firstStatusId = statuses.length > 0 ? statuses[0].id : null;

    if (!firstStatusId) {
      setError("Błąd konfiguracji systemu: brak zdefiniowanych statusów w bazie.");
      setLoading(false);
      return;
    }

    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          userId: user.id,
          statusId: firstStatusId,
          priorityId: 2,
          categoryId: Number(form.categoryId),
          assigneeId: form.assigneeId ? Number(form.assigneeId) : null,
          title: form.title.trim(),
          description: form.description.trim(),
        }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || "Wystąpił błąd podczas wysyłania zgłoszenia");
      }

      router.push("/");
      router.refresh();
    } catch (err: any) {
      setError(err.message);
      setLoading(false);
    }
  };

  return (
    <div className="mx-auto px-6 py-12 max-w-2xl container">
      <header className="mb-10 text-center">
        <h1 className="font-bold text-slate-900 text-2xl uppercase tracking-tighter">Nowe zgłoszenie</h1>
        <p className="text-slate-500 text-sm">Opisz swój problem, a nasz zespół zajmie się nim najszybciej jak to możliwe.</p>
      </header>

      <form onSubmit={handleSubmit} className="space-y-6">
        {error && <div className="bg-red-50 slide-in-from-top-1 p-4 border border-red-100 rounded-xl font-bold text-[11px] text-red-600 text-center uppercase tracking-widest animate-in fade-in">⚠️ {error}</div>}

        <div className="gap-6 grid grid-cols-1 md:grid-cols-2">
          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Kategoria</Label>
            <select name="categoryId" value={form.categoryId} onChange={handleChange} className="flex bg-white px-4 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-slate-900 w-full h-11 font-medium text-sm transition-all appearance-none cursor-pointer">
              <option value="">Wybierz...</option>
              {categories.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.name}
                </option>
              ))}
            </select>
          </div>

          <div className="space-y-2">
            <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Przypisanie</Label>
            <select name="assigneeId" value={form.assigneeId} onChange={handleChange} className="flex bg-white px-4 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-slate-900 w-full h-11 font-medium text-sm transition-all appearance-none cursor-pointer">
              <option value="">Automatycznie</option>
              {assignees.map((u) => (
                <option key={u.id} value={u.id}>
                  {u.name || u.email}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className="space-y-2">
          <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Tytuł wiadomości</Label>
          <Input name="title" placeholder="Krótki temat (min. 5 znaków)..." value={form.title} onChange={handleChange} className="shadow-none border-slate-200 rounded-xl focus:ring-2 focus:ring-slate-900 h-11 font-medium" />
        </div>

        <div className="space-y-2">
          <Label className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">Opis sytuacji</Label>
          <Textarea name="description" placeholder="Szczegóły problemu (min. 10 znaków)..." value={form.description} onChange={handleChange} className="shadow-none p-4 border-slate-200 rounded-xl focus:ring-2 focus:ring-slate-900 min-h-[140px] font-medium resize-none" />
        </div>

        <div className="flex justify-end pt-4">
          <Button type="submit" disabled={loading} className="bg-slate-900 shadow-slate-100 shadow-xl hover:shadow-slate-200 px-10 rounded-full h-12 font-bold text-[11px] text-white uppercase tracking-widest hover:scale-[0.98] transition-all">
            {loading ? <Loader2 className="w-4 h-4 animate-spin" /> : "Wyślij zgłoszenie"}
          </Button>
        </div>
      </form>
    </div>
  );
}

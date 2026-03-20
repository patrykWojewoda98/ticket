"use client";

import { useEffect, useState } from "react";

export default function NewTicketPage() {
  const [form, setForm] = useState({
    assigneeId: "",
    categoryId: "",
    title: "",
    description: "",
  });

  const [categories, setCategories] = useState<any[]>([]);
  const [assignees, setAssignees] = useState<any[]>([]);

  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [catRes, userRes] = await Promise.all([
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/TicketCategory`),
          fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/User`),
        ]);

        if (catRes.ok) setCategories(await catRes.json());
        if (userRes.ok) {
  const users = await userRes.json();

  const admins = users.filter(
    (u: any) => u.role === "Admin" || u.isAdmin === true
  );

  setAssignees(admins);
}
      } catch (err) {
        console.error("Błąd pobierania danych:", err);
      }
    };

    fetchData();
  }, []);

  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    // ✅ WALIDACJA FRONTENDOWA (dopasowana do backendu)
    if (form.title.trim().length < 5) {
      setError("Tytuł musi mieć co najmniej 5 znaków");
      setLoading(false);
      return;
    }

    if (form.description.trim().length < 10) {
      setError("Opis musi mieć co najmniej 10 znaków");
      setLoading(false);
      return;
    }

    try {
      const response = await fetch(
        `${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            userId: 1,
            statusId: 1,
            priorityId: 2,
            categoryId: form.categoryId
              ? Number(form.categoryId)
              : null,
            assigneeId: form.assigneeId
              ? Number(form.assigneeId)
              : null,
            title: form.title,
            description: form.description,
          }),
        }
      );

      if (!response.ok) {
        const text = await response.text();
        throw new Error(text || "Błąd tworzenia ticketu");
      }

      setSuccess(true);

      setForm({
        assigneeId: "",
        categoryId: "",
        title: "",
        description: "",
      });
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-xl mx-auto mt-24 px-4">
      <div className="border rounded-2xl p-6 shadow-lg">
        <h1 className="text-2xl font-semibold mb-6">Nowy Ticket</h1>

        <form onSubmit={handleSubmit} className="space-y-4">
          {/* CATEGORY */}
          <select
            name="categoryId"
            value={form.categoryId}
            onChange={handleChange}
            className="w-full border p-2 rounded-xl"
            required
          >
            <option value="">Wybierz kategorię</option>
            {categories.map((c) => (
              <option key={c.id} value={c.id}>
                {c.name}
              </option>
            ))}
          </select>

          {/* ASSIGNEE */}
          <select
            name="assigneeId"
            value={form.assigneeId}
            onChange={handleChange}
            className="w-full border p-2 rounded-xl"
          >
            <option value="">Brak przypisania</option>
            {assignees.map((u) => (
              <option key={u.id} value={u.id}>
                {u.name || u.email}
              </option>
            ))}
          </select>

          {/* TITLE */}
          <input
            name="title"
            placeholder="Tytuł (min. 5 znaków)"
            value={form.title}
            onChange={handleChange}
            minLength={5}
            className="w-full border p-2 rounded-xl"
            required
          />

          {/* DESCRIPTION */}
          <textarea
            name="description"
            placeholder="Opis (min. 10 znaków)"
            value={form.description}
            onChange={handleChange}
            minLength={10}
            className="w-full border p-2 rounded-xl"
            rows={4}
            required
          />

          <button
            type="submit"
            disabled={loading}
            className="w-full bg-black text-white py-2 rounded-xl"
          >
            {loading ? "Tworzenie..." : "Utwórz Ticket"}
          </button>

          {success && (
            <p className="text-green-600">Ticket został utworzony ✅</p>
          )}

          {error && <p className="text-red-600">{error}</p>}
        </form>
      </div>
    </div>
  );
}
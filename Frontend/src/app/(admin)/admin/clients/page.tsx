"use client";

import ClientsTable, { User } from "@/components/users/ClientsTable";
import React, { useEffect, useState } from "react";

const API = "http://localhost:5229/api/user";

export default function ClientsPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchUsers = async () => {
    setLoading(true);
    setError(null);

    try {
      const res = await fetch(API);
      if (!res.ok) throw new Error(`Błąd ${res.status}`);

      const data = await res.json();

      const mappedUsers = data.map((u: any) => ({
        ...u,
        emailVerified: !!u.emailVerified,
      }));

      setUsers(mappedUsers);
    } catch (err: any) {
      setError(err.message || "Nieznany błąd");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleDelete = async (id: number) => {
    if (!confirm("Na pewno usunąć użytkownika?")) return;

    try {
      const res = await fetch(`${API}/${id}`, {
        method: "DELETE",
      });

      if (!res.ok) throw new Error("Błąd usuwania");

      // usuń z UI bez reload
      setUsers((prev) => prev.filter((u) => u.id !== id));
    } catch (err) {
      console.error(err);
      alert("Nie udało się usunąć użytkownika");
    }
  };

  return (
    <div className="mt-16">
      <div className="flex justify-between mb-6">
        <h1 className="text-2xl font-bold">Clients</h1>
      </div>

      {loading && <p className="text-gray-500">Ładowanie danych...</p>}
      {error && <p className="text-red-500">Błąd: {error}</p>}

      {!loading && !error && (
        <ClientsTable users={users} onDelete={handleDelete} />
      )}
    </div>
  );
}
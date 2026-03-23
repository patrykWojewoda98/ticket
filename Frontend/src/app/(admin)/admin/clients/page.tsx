"use client";

import ClientsTable, { User, Company } from "@/components/users/ClientsTable";
import React, { useEffect, useState, useMemo } from "react";

const API_USERS = `${process.env.NEXT_PUBLIC_APP_URL}/api/user`;
const API_COMPANIES = `${process.env.NEXT_PUBLIC_APP_URL}/api/company`;

export default function ClientsPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [companies, setCompanies] = useState<Company[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchData = async () => {
    setLoading(true);
    setError(null);

    try {
      const [usersRes, companiesRes] = await Promise.all([
        fetch(API_USERS),
        fetch(API_COMPANIES),
      ]);

      if (!usersRes.ok || !companiesRes.ok) {
        throw new Error("Błąd pobierania danych");
      }

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

  // 🔥 mapa companyId → companyName
  const companyMap = useMemo(() => {
    return Object.fromEntries(
      companies.map((c) => [c.id, c.name])
    );
  }, [companies]);

  const handleDelete = async (id: number) => {
    if (!confirm("Na pewno usunąć użytkownika?")) return;

    try {
      const res = await fetch(`${API_USERS}/${id}`, {
        method: "DELETE",
      });

      if (!res.ok) throw new Error("Błąd usuwania");

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
        <ClientsTable
          users={users}
          companyMap={companyMap} // 👈 przekazujemy mapę
          onDelete={handleDelete}
        />
      )}
    </div>
  );
}
"use client";

import React, { useState, useEffect } from "react";

// Twoje DTO zgodne z bazą danych
export interface Account {
  id: number;
  userId: number;
  providerId: number;
  accountId: number;
  scope?: string;
}

const API_URL = "http://localhost:5229/api/account";

export default function AccountPage() {
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [loading, setLoading] = useState(true);
  const [formData, setFormData] = useState({ userId: 0, providerId: 0, accountId: 0 });

  // --- 1. POBIERANIE (READ) ---
  const loadAccounts = async () => {
    setLoading(true);
    try {
      const res = await fetch(API_URL, { cache: "no-store" });
      if (res.ok) {
        const data = await res.json();
        setAccounts(Array.isArray(data) ? data : []);
      }
    } catch (e) {
      console.error("Błąd połączenia z API");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadAccounts();
  }, []);

  // --- 2. TWORZENIE (CREATE) ---
  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault();
    const res = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(formData),
    });
    if (res.ok) {
      setFormData({ userId: 0, providerId: 0, accountId: 0 }); // reset form
      loadAccounts();
    }
  };

  // --- 3. USUWANIE (DELETE) ---
  const handleDelete = async (id: number) => {
    if (!confirm("Na pewno usunąć?")) return;
    const res = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
    if (res.ok) loadAccounts();
  };

  // --- 4. AKTUALIZACJA (UPDATE) ---
  const handleUpdate = async (account: Account) => {
    const newProvider = prompt("Wpisz nowy ProviderId:", account.providerId.toString());
    if (newProvider === null) return;

    const updatedAccount = { ...account, providerId: Number(newProvider) };

    const res = await fetch(`${API_URL}/${account.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(updatedAccount),
    });
    if (res.ok) loadAccounts();
  };

  return (
    <div className="space-y-10 mx-auto p-10 max-w-4xl">
      <h1 className="pb-4 border-b font-bold text-3xl">Zarządzanie Kontami</h1>

      {/* --- FORMA CREATE --- */}
      <section className="bg-gray-50 p-6 border rounded-xl">
        <h2 className="mb-4 font-semibold text-blue-600 text-xl">Dodaj konto</h2>
        <form onSubmit={handleCreate} className="flex items-end gap-4">
          <div>
            <label className="block text-gray-500 text-xs">User ID</label>
            <input type="number" className="p-2 border rounded w-24" value={formData.userId} onChange={(e) => setFormData({ ...formData, userId: Number(e.target.value) })} />
          </div>
          <div>
            <label className="block text-gray-500 text-xs">Provider ID</label>
            <input type="number" className="p-2 border rounded w-24" value={formData.providerId} onChange={(e) => setFormData({ ...formData, providerId: Number(e.target.value) })} />
          </div>
          <div>
            <label className="block text-gray-500 text-xs">Account ID</label>
            <input type="number" className="p-2 border rounded w-24" value={formData.accountId} onChange={(e) => setFormData({ ...formData, accountId: Number(e.target.value) })} />
          </div>
          <button type="submit" className="bg-blue-600 hover:bg-blue-700 px-6 py-2 rounded font-bold text-white">
            STWÓRZ
          </button>
        </form>
      </section>

      {/* --- LISTA I TESTY (READ/DELETE/UPDATE) --- */}
      <section className="space-y-4">
        <h2 className="font-semibold text-green-600 text-xl">Konta w bazie ({accounts.length})</h2>
        {loading ? (
          <p>Ładowanie...</p>
        ) : (
          <div className="gap-4 grid">
            {accounts.map((acc) => (
              <div key={acc.id} className="flex justify-between items-center bg-white shadow-sm p-4 border rounded-lg">
                <div>
                  <p className="font-mono text-gray-400 text-sm">#ID: {acc.id}</p>
                  <p>
                    <strong>Provider:</strong> {acc.providerId} | <strong>User:</strong> {acc.userId}
                  </p>
                  <p className="text-gray-500 text-xs italic">Scope: {acc.scope || "brak"}</p>
                </div>
                <div className="flex gap-2">
                  <button onClick={() => handleUpdate(acc)} className="bg-yellow-500 px-3 py-1 rounded font-bold text-white text-sm">
                    EDYTUJ
                  </button>
                  <button onClick={() => handleDelete(acc.id)} className="bg-red-500 px-3 py-1 rounded font-bold text-white text-sm">
                    USUŃ
                  </button>
                </div>
              </div>
            ))}
            {accounts.length === 0 && <p className="text-gray-400">Baza jest pusta.</p>}
          </div>
        )}
      </section>

      {/* --- DEBUG JSON --- */}
      <details className="mt-10">
        <summary className="text-gray-400 text-sm cursor-pointer">Pokaż surowy JSON</summary>
        <pre className="bg-gray-100 mt-2 p-4 rounded overflow-auto text-xs">{JSON.stringify(accounts, null, 2)}</pre>
      </details>
    </div>
  );
}

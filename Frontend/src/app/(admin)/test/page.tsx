"use client";
import React, { useState, useEffect } from "react";

const API = "http://localhost:5229/api/account";

export default function CompactAccountPage() {
  const [data, setData] = useState<any>(null);
  const [id, setId] = useState("1");
  const [pId, setPId] = useState("1"); // ProviderID
  const [form, setForm] = useState({ userId: 1, providerId: 1, accountId: 1 });

  const fetcher = async (url: string, method = "GET", body?: any) => {
    try {
      const res = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: body ? JSON.stringify(body) : undefined,
      });
      const result = res.ok ? await res.json() : { error: res.status };
      setData(result);
    } catch (e) {
      setData({ error: "API DOWN" });
    }
  };

  return (
    <div className="space-y-6 mx-auto p-8 max-w-5xl font-sans">
      <h1 className="mb-4 font-bold text-2xl">Account API Tester 🛠️</h1>

      <div className="gap-4 grid grid-cols-1 md:grid-cols-2">
        {/* --- LEWA KOLUMNA: AKCJE (INPUTY) --- */}
        <div className="space-y-4">
          <section className="bg-blue-50 p-4 border rounded">
            <h3 className="mb-2 font-bold">Pobieranie (GET)</h3>
            <div className="flex gap-2 mb-4">
              <input type="number" value={id} onChange={(e) => setId(e.target.value)} className="p-1 border w-16" placeholder="ID" />
              <input type="number" value={pId} onChange={(e) => setPId(e.target.value)} className="p-1 border w-16" placeholder="PID" />
            </div>
            <div className="flex flex-wrap gap-2">
              <button onClick={() => fetcher(API)} className="bg-blue-500 px-3 py-1 rounded text-white text-sm">
                All
              </button>
              <button onClick={() => fetcher(`${API}/${id}`)} className="bg-blue-600 px-3 py-1 rounded text-white text-sm">
                By ID ({id})
              </button>
              <button onClick={() => fetcher(`${API}/user/${id}`)} className="bg-blue-700 px-3 py-1 rounded text-white text-sm">
                By User ({id})
              </button>
              <button onClick={() => fetcher(`${API}/provider/${pId}/${id}`)} className="bg-blue-800 px-3 py-1 rounded text-white text-sm">
                By Prov ({pId}/{id})
              </button>
            </div>
          </section>

          <section className="bg-green-50 p-4 border rounded">
            <h3 className="mb-2 font-bold">Create / Delete / Update</h3>
            <div className="flex gap-2 mb-2">
              <input type="number" placeholder="UID" className="p-1 border w-full" onChange={(e) => setForm({ ...form, userId: +e.target.value })} />
              <input type="number" placeholder="PID" className="p-1 border w-full" onChange={(e) => setForm({ ...form, providerId: +e.target.value })} />
              <input type="number" placeholder="AID" className="p-1 border w-full" onChange={(e) => setForm({ ...form, accountId: +e.target.value })} />
            </div>
            <div className="flex gap-2">
              <button onClick={() => fetcher(API, "POST", form)} className="bg-green-600 px-4 py-1 rounded w-full text-white text-sm">
                CREATE
              </button>
              <button onClick={() => fetcher(`${API}/${id}`, "PUT", { ...form, id: +id })} className="bg-yellow-600 px-4 py-1 rounded w-full text-white text-sm">
                UPDATE ({id})
              </button>
              <button onClick={() => fetcher(`${API}/${id}`, "DELETE")} className="bg-red-600 px-4 py-1 rounded w-full text-white text-sm">
                DELETE ({id})
              </button>
            </div>
          </section>
        </div>

        {/* --- PRAWA KOLUMNA: WYNIK (WIDOK JSON) --- */}
        <div className="bg-gray-900 p-4 border rounded min-h-[400px] overflow-auto text-green-400">
          <h3 className="flex justify-between mb-2 pb-2 border-gray-700 border-b font-mono text-white text-sm">
            API RESPONSE:{" "}
            <button onClick={() => setData(null)} className="text-red-400 text-xs underline">
              Clear
            </button>
          </h3>
          <pre className="text-xs">{JSON.stringify(data, null, 2)}</pre>
        </div>
      </div>

      <footer className="text-gray-400 text-xs text-center italic">Status: {data?.error ? "❌ ERROR: " + data.error : "✅ OK / READY"}</footer>
    </div>
  );
}

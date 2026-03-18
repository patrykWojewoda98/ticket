"use client";
import React, { useState } from "react";

const API = "http://localhost:5229/api/account";

export default function BetterAccountPage() {
  const [data, setData] = useState<any>(null);
  const [loading, setLoading] = useState(false);

  // Zaktualizowane stany o pola tekstowe (string)
  const [createForm, setCreateForm] = useState({
    userId: 0,
    providerId: "",
    password: "",
  });

  const [updateForm, setUpdateForm] = useState({
    id: 0,
    userId: 0,
    providerId: "",
    password: "",
  });

  const [deleteId, setDeleteId] = useState("");
  const [getInputs, setGetInputs] = useState({ id: "", pId: "" });

  const fetcher = async (url: string, method = "GET", body?: any) => {
    setLoading(true);
    try {
      const res = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: body ? JSON.stringify(body) : undefined,
      });

      const result = res.ok ? (res.status === 204 ? { message: "Success (No Content)" } : await res.json()) : { status: res.status, error: res.statusText, details: await res.text() };

      setData(result);
    } catch (e) {
      setData({ error: "CONNECTION_ERROR" });
    } finally {
      setLoading(false);
    }
  };

  const inputClass = "mb-2 p-2 border rounded outline-none focus:ring-2 focus:ring-blue-300 w-full text-black text-sm";
  const labelClass = "block mb-1 font-bold text-[10px] text-gray-500 uppercase";

  return (
    <div className="flex md:flex-row flex-col gap-6 bg-gray-50 mx-auto p-6 max-w-7xl min-h-screen font-sans">
      {/* --- LEWA KOLUMNA: TESTER --- */}
      <div className="space-y-6 w-full md:w-1/2">
        <h1 className="font-black text-gray-800 text-3xl tracking-tight">
          API TESTER <span className="text-blue-600">v2.1</span>
        </h1>

        {/* SEKCOJA GET */}
        <div className="bg-white shadow-sm p-4 border rounded-xl">
          <h2 className="flex items-center gap-2 mb-3 font-bold text-blue-600">🔍 POBIERANIE</h2>
          <div className="gap-2 grid grid-cols-2 mb-3">
            <div>
              <label className={labelClass}>ID / User ID</label>
              <input className={inputClass} type="number" value={getInputs.id} onChange={(e) => setGetInputs({ ...getInputs, id: e.target.value })} placeholder="np. 1" />
            </div>
            <div>
              <label className={labelClass}>Provider ID (String)</label>
              <input className={inputClass} type="text" value={getInputs.pId} onChange={(e) => setGetInputs({ ...getInputs, pId: e.target.value })} placeholder="np. google" />
            </div>
          </div>
          <div className="flex flex-wrap gap-2">
            <button onClick={() => fetcher(API)} className="bg-blue-500 hover:bg-blue-600 px-3 py-1 rounded font-bold text-white text-xs uppercase transition">
              Wszystkie
            </button>
            <button onClick={() => fetcher(`${API}/${getInputs.id}`)} className="bg-blue-600 hover:bg-blue-700 disabled:opacity-50 px-3 py-1 rounded font-bold text-white text-xs uppercase transition" disabled={!getInputs.id}>
              Po ID
            </button>
            <button onClick={() => fetcher(`${API}/user/${getInputs.id}`)} className="bg-blue-700 hover:bg-blue-800 disabled:opacity-50 px-3 py-1 rounded font-bold text-white text-xs uppercase transition" disabled={!getInputs.id}>
              Po UserID
            </button>
          </div>
        </div>

        {/* SEKCJA CREATE */}
        <div className="bg-white shadow-sm p-4 border border-l-4 border-l-green-500 rounded-xl">
          <h2 className="flex items-center gap-2 mb-3 font-bold text-green-600">➕ DODAWANIE (POST)</h2>
          <div className="gap-2 grid grid-cols-2">
            <input className={inputClass} type="number" placeholder="User ID" onChange={(e) => setCreateForm({ ...createForm, userId: +e.target.value })} />
            <input className={inputClass} type="text" placeholder="Provider (np. google)" onChange={(e) => setCreateForm({ ...createForm, providerId: e.target.value })} />
          </div>
          <input className={inputClass} type="password" placeholder="Password" onChange={(e) => setCreateForm({ ...createForm, password: e.target.value })} />
          <button onClick={() => fetcher(API, "POST", createForm)} className="bg-green-600 hover:bg-green-700 py-2 rounded w-full font-bold text-white text-sm uppercase transition">
            Wyślij POST
          </button>
        </div>

        {/* SEKCJA UPDATE */}
        <div className="bg-white shadow-sm p-4 border border-l-4 border-l-yellow-500 rounded-xl">
          <h2 className="flex items-center gap-2 mb-3 font-bold text-yellow-600">✏️ EDYCJA (PUT)</h2>
          <div className="gap-2 grid grid-cols-2">
            <input className={inputClass} type="number" placeholder="ID rekordu" onChange={(e) => setUpdateForm({ ...updateForm, id: +e.target.value })} />
            <input className={inputClass} type="number" placeholder="Nowe User ID" onChange={(e) => setUpdateForm({ ...updateForm, userId: +e.target.value })} />
          </div>
          <div className="gap-2 grid grid-cols-2">
            <input className={inputClass} type="text" placeholder="Nowy Provider" onChange={(e) => setUpdateForm({ ...updateForm, providerId: e.target.value })} />
            <input className={inputClass} type="password" placeholder="Nowe Password" onChange={(e) => setUpdateForm({ ...updateForm, password: e.target.value })} />
          </div>
          <button onClick={() => fetcher(`${API}/${updateForm.id}`, "PUT", updateForm)} className="bg-yellow-600 hover:bg-yellow-700 py-2 rounded w-full font-bold text-white text-sm uppercase transition">
            Wyślij PUT
          </button>
        </div>

        {/* SEKCJA DELETE */}
        <div className="bg-white shadow-sm p-4 border border-l-4 border-l-red-500 rounded-xl">
          <h2 className="flex items-center gap-2 mb-3 font-bold text-red-600">🗑️ USUWANIE (DELETE)</h2>
          <div className="flex gap-2">
            <input className={inputClass} type="number" placeholder="ID do usunięcia" value={deleteId} onChange={(e) => setDeleteId(e.target.value)} />
            <button onClick={() => fetcher(`${API}/${deleteId}`, "DELETE")} className="bg-red-600 hover:bg-red-700 disabled:opacity-50 px-6 py-2 rounded font-bold text-white text-sm uppercase transition" disabled={!deleteId}>
              Usuń
            </button>
          </div>
        </div>
      </div>

      {/* --- PRAWA KOLUMNA: KONSOLA --- */}
      <div className="top-6 sticky flex flex-col flex-1 h-[calc(100vh-3rem)]">
        <div className="flex flex-col bg-gray-900 shadow-2xl border border-gray-700 rounded-xl h-full overflow-hidden">
          <div className="flex justify-between items-center bg-gray-800 p-3 border-gray-700 border-b">
            <span className="flex items-center gap-2 font-mono text-gray-400 text-xs uppercase tracking-widest">
              <span className={`w-2 h-2 rounded-full ${loading ? "bg-yellow-500 animate-ping" : "bg-green-500"}`}></span>
              Console Response
            </span>
            <button onClick={() => setData(null)} className="text-gray-500 hover:text-white text-xs transition">
              CLEAR
            </button>
          </div>
          <div className="flex-1 p-4 overflow-auto font-mono text-[11px] leading-relaxed">{data ? <pre className={data.error ? "text-red-400" : "text-green-400"}>{JSON.stringify(data, null, 2)}</pre> : <div className="flex justify-center items-center h-full text-gray-600 italic">Oczekiwanie na zapytanie...</div>}</div>
        </div>
      </div>
    </div>
  );
}

"use client";

import Link from "next/link";
import { Button } from "@/components/ui/button";
import { useLoginForm } from "@/hooks/useLoginForm";

export function LoginForm() {
  const { email, password, loading, setEmail, setPassword, handleSubmit } = useLoginForm();

  return (
    <div className="flex flex-col gap-6 shadow-sm p-8 border rounded-2xl">
      <h1 className="font-bold text-slate-900 text-2xl text-center">Zaloguj się</h1>

      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">Email</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="twoj@email.com" className="p-2 border rounded-lg text-sm" required />
        </div>

        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">Hasło</label>
          <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Wpisz hasło" className="p-2 border rounded-lg text-sm" required />
        </div>

        <Button type="submit" disabled={loading}>
          {loading ? "Logowanie..." : "Zaloguj się"}
        </Button>
      </form>
    </div>
  );
}

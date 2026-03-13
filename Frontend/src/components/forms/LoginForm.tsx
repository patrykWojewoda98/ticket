"use client";

import Link from "next/link";
import { Button } from "@/components/ui/button";
import { useLoginForm } from "@/hooks/useLoginForm";

export function LoginForm() {
  const { email, password, loading, setEmail, setPassword, handleSubmit } =
    useLoginForm();

  return (
    <div className="border rounded-2xl p-8 shadow-sm flex flex-col gap-6">
      <h1 className="text-2xl font-bold text-slate-900 text-center">
        Zaloguj się
      </h1>

      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">Email</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="twoj@email.com"
            className="border rounded-lg p-2 text-sm"
            required
          />
        </div>

        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">Hasło</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Wpisz hasło"
            className="border rounded-lg p-2 text-sm"
            required
          />
        </div>

        <Button
          type="submit"
          disabled={loading}
          className="transition-all duration-200 hover:scale-105 active:scale-95"
        >
          {loading ? "Logowanie..." : "Zaloguj się"}
        </Button>
      </form>

      <p className="text-sm text-slate-500 text-center">
        Nie masz konta?{" "}
        <Link
          href="/register"
          className="text-slate-900 font-medium hover:underline"
        >
          Zarejestruj się
        </Link>
      </p>
    </div>
  );
}
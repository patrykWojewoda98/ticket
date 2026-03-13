"use client";

import Link from "next/link";
import { Button } from "@/components/ui/button";
import { useRegisterForm } from "@/hooks/useRegisterForm";

export function RegisterForm() {
  const {
    email,
    password,
    confirmPassword,
    showPassword,
    loading,
    setEmail,
    setPassword,
    setConfirmPassword,
    setShowPassword,
    handleSubmit,
  } = useRegisterForm();

  return (
    <div className="border rounded-2xl p-8 shadow-sm flex flex-col gap-6">
      <h1 className="text-2xl font-bold text-slate-900 text-center">
        Utwórz konto
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
            type={showPassword ? "text" : "password"}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Minimum 8 znaków"
            className="border rounded-lg p-2 text-sm"
            required
          />
        </div>

        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">
            Powtórz hasło
          </label>
          <input
            type={showPassword ? "text" : "password"}
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            placeholder="Powtórz hasło"
            className="border rounded-lg p-2 text-sm"
            required
          />
        </div>

        <button
          type="button"
          onClick={() => setShowPassword((prev) => !prev)}
          className="text-xs text-slate-500 hover:text-slate-700 w-fit"
        >
          {showPassword ? "Ukryj hasło" : "Pokaż hasło"}
        </button>

        <Button
          type="submit"
          disabled={loading}
          className="transition-all duration-200 hover:scale-105 active:scale-95"
        >
          {loading ? "Tworzenie konta..." : "Zarejestruj się"}
        </Button>
      </form>

      <p className="text-sm text-slate-500 text-center">
        Masz już konto?{" "}
        <Link
          href="/login"
          className="text-slate-900 font-medium hover:underline"
        >
          Zaloguj się
        </Link>
      </p>
    </div>
  );
}
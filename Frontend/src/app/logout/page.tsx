"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/components/common/AuthContext";

export default function LogoutPage() {
  const router = useRouter();
  const { setIsAuthenticated } = useAuth();

  useEffect(() => {
    // 1. Czyścimy dane z localStorage
    localStorage.removeItem("user");

    // 2. Czyścimy ciasteczko roli (KLUCZOWE dla Middleware)
    // Ustawiamy datę wygaśnięcia na 1970 rok, co zmusza przeglądarkę do usunięcia ciastka
    document.cookie = "user_role=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT";

    // 3. Czyścimy stan globalny
    setIsAuthenticated(false);

    // 4. Przekierowanie
    const timeout = setTimeout(() => {
      // Używamy window.location.href zamiast router.push,
      // aby wymusić pełne odświeżenie strony i "reset" Middleware
      window.location.href = "/";
    }, 1500);

    return () => clearTimeout(timeout);
  }, [setIsAuthenticated]);

  return (
    <div className="flex justify-center items-center bg-slate-50 min-h-screen">
      <div className="bg-white shadow-sm p-10 border border-slate-200 rounded-2xl text-center">
        <div className="flex justify-center mb-4">
          {/* Opcjonalny spinner/ikona */}
          <div className="border-4 border-slate-200 border-t-slate-900 rounded-full w-8 h-8 animate-spin"></div>
        </div>
        <h1 className="mb-2 font-bold text-slate-900 text-2xl">Wylogowywanie...</h1>
        <p className="text-slate-600">Za chwilę zostaniesz bezpiecznie przekierowany.</p>
      </div>
    </div>
  );
}

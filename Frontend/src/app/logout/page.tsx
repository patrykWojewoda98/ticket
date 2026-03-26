"use client";

import { useEffect } from "react";
import { useAuth } from "@/components/common/AuthContext";
import { Loader2, Ticket } from "lucide-react";

export default function LogoutPage() {
  const { setIsAuthenticated } = useAuth();

  useEffect(() => {
    // Synchronizacja czyszczenia danych
    const performLogout = () => {
      localStorage.removeItem("user");
      document.cookie = "user_role=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT";
      setIsAuthenticated(false);

      // Krótkie opóźnienie (500ms), żeby loader nie mignął zbyt szybko (co wygląda jak błąd)
      setTimeout(() => {
        window.location.href = "/";
      }, 500);
    };

    performLogout();
  }, [setIsAuthenticated]);

  return (
    <div className="z-[9999] fixed inset-0 flex flex-col justify-center items-center bg-white">
      <div className="flex flex-col items-center gap-4 animate-in duration-300 fade-in">
        <div className="relative flex justify-center items-center">
          <div className="absolute bg-slate-100 opacity-75 rounded-full w-12 h-12 animate-ping"></div>
          <div className="relative bg-white shadow-sm p-3 border border-slate-100 rounded-2xl">
            <Ticket className="w-6 h-6 text-slate-900" />
          </div>
        </div>

        {/* Napis i Loader */}
        <div className="flex items-center gap-2">
          <Loader2 className="w-4 h-4 text-slate-400 animate-spin" />
          <span className="font-black text-[10px] text-slate-400 uppercase tracking-[0.2em]">Wylogowywanie...</span>
        </div>
      </div>
    </div>
  );
}

"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/components/common/AuthContext";

export default function LogoutPage() {
  const router = useRouter();
  const { setIsAuthenticated } = useAuth();

  useEffect(() => {
    localStorage.removeItem("user");
    setIsAuthenticated(false);

    const timeout = setTimeout(() => {
      router.push("/");
    }, 2000);

    return () => clearTimeout(timeout);
  }, [router, setIsAuthenticated]);

  return (
    <div className="flex items-center justify-center min-h-screen bg-slate-50">
      <div className="bg-white border border-slate-200 rounded-2xl p-10 shadow-sm text-center">
        <h1 className="text-2xl font-bold mb-2">
          Wylogowywanie...
        </h1>
        <p className="text-slate-600">
          Za chwilę zostaniesz przekierowany
        </p>
      </div>
    </div>
  );
}
"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/components/common/AuthContext";

export function useLoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  // Wyciągamy potrzebne funkcje z Twojego Contextu
  const { setIsAuthenticated, setUser } = useAuth();
  const router = useRouter();
  const apiBaseUrl = process.env.NEXT_PUBLIC_APP_URL?.replace(/\/$/, "");

  const safeParseJson = async (response: Response) => {
    const contentType = response.headers.get("content-type") || "";
    if (!contentType.includes("application/json")) {
      const text = await response.text();
      throw new Error(text || "Odpowiedź serwera nie jest JSON-em");
    }
    return response.json();
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      if (!apiBaseUrl) {
        throw new Error("Brak konfiguracji NEXT_PUBLIC_APP_URL");
      }
      // 1. Logowanie
      const loginRes = await fetch(`${apiBaseUrl}/api/user/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (!loginRes.ok) {
        if (loginRes.status === 423) {
           const errData = await safeParseJson(loginRes);
           throw new Error(errData.message || "Zbyt wiele nieudanych prób logowania. Konto zostało zablokowane na 15 minut.");
        }
        throw new Error("Błędne dane logowania");
      }

      const data = await safeParseJson(loginRes);

      // --- TO ROZWIĄZUJE PROBLEM F5 ---
      // 1. Zapisujemy w localStorage (na przyszłość, po odświeżeniu)
      localStorage.setItem("user", JSON.stringify(data));

      // 2. Ustawiamy ciasteczko (jeśli używasz go w middleware)
      document.cookie = `user_role=${data.role}; path=/; max-age=86400`;

      // 3. AKTUALIZUJEMY STAN REACTA (To sprawia, że Navbar/Sidebar widzą zmiany od razu!)
      setUser(data);
      setIsAuthenticated(true);

      // 4. Przekierowanie (używamy router.push zamiast window.location dla płynności)
      if (data.role?.toLowerCase() === "admin") {
        router.push("/admin/tickets");
      } else {
        router.push("/");
      }
    } catch (error: any) {
      alert(error.message || "Błąd logowania");
    } finally {
      setLoading(false);
    }
  };

  return { email, password, loading, setEmail, setPassword, handleSubmit };
}

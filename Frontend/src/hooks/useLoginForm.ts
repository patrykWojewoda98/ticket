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

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      // 1. Pobranie użytkowników (Twój flow)
      const usersRes = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/user`);
      if (!usersRes.ok) throw new Error("Błąd pobierania użytkowników");

      const users = await usersRes.json();
      const foundUser = users.find((u: any) => u.email === email);
      if (!foundUser) throw new Error("Nie znaleziono użytkownika");

      // 2. Logowanie
      const loginRes = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/user/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id: foundUser.id, password }),
      });

      if (!loginRes.ok) throw new Error("Błędne dane logowania");

      const data = await loginRes.json();

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

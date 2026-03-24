"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/components/common/AuthContext";

export function useLoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const { setIsAuthenticated } = useAuth();
  const router = useRouter();

  const resetForm = () => {
    setEmail("");
    setPassword("");
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      const usersRes = await fetch(
        `${process.env.NEXT_PUBLIC_APP_URL}/api/user`
      );

      if (!usersRes.ok) {
        throw new Error("Nie udało się pobrać użytkowników");
      }

      const users = await usersRes.json();

      const user = users.find((u: any) => u.email === email);

      if (!user) {
        throw new Error("Nie znaleziono użytkownika");
      }

      const loginRes = await fetch(
        `${process.env.NEXT_PUBLIC_APP_URL}/api/user/login`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: user.id,
            password,
          }),
        }
      );

      if (!loginRes.ok) {
        throw new Error("Błędne dane logowania");
      }

      const data = await loginRes.json();

      // 🔹 zapis danych
      localStorage.setItem("user", JSON.stringify(data));

      // 🔹 cookie (dla middleware)
      document.cookie = `user_role=${data.role}; path=/; max-age=86400`;

      // 🔹 ustaw auth
      setIsAuthenticated(true);

      // 🔹 REDIRECT na podstawie roli
      if (data.role === "admin") {
        router.push("/admin");
      } else {
        router.push("/");
      }

      resetForm();
    } catch (error: any) {
      console.error(error);
      alert(error.message || "Błąd logowania");
    } finally {
      setLoading(false);
    }
  };

  return {
    email,
    password,
    loading,
    setEmail,
    setPassword,
    handleSubmit,
  };
}
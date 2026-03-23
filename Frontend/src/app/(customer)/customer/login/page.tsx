"use client";

import { useState } from "react";
import { useAuth } from "@/components/common/AuthContext";

export function useLoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const { setIsAuthenticated } = useAuth(); // ✅ DODANE

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
        throw new Error("Błąd pobierania użytkowników");
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

      console.log("Zalogowano:", data);
      console.log("Zalogowano:", data);

      // ✅ GLOBALNY STATE
      setIsAuthenticated(true);

      localStorage.setItem("user", JSON.stringify(data));

      resetForm(); // ✅ działa teraz
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

import { LoginForm } from "@/components/forms/LoginForm";
export default function Page() {
  return (
    <div className="flex justify-center items-center min-h-screen">
      <LoginForm />
    </div>
  );
}
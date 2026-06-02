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
      const loginRes = await fetch(`${process.env.NEXT_PUBLIC_APP_URL?.replace(/\/$/, "")}/api/user/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email,
          password,
        }),
      });

      if (!loginRes.ok) {
        if (loginRes.status === 423) {
           const errData = await loginRes.json();
           throw new Error(errData.message || "Zbyt wiele nieudanych prób logowania. Konto zostało zablokowane na 15 minut.");
        }
        throw new Error("Błędne dane logowania");
      }

      const data = await loginRes.json();

      console.log("Zalogowano:", data);

      setIsAuthenticated(true);

      localStorage.setItem("user", JSON.stringify(data));

      
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

import { LoginForm } from "@/components/forms/LoginForm";

export default function Page() {
  return (
    <div className="flex justify-center items-center py-12">
      <LoginForm />
    </div>
  );
}

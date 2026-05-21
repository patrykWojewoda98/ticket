"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/components/common/AuthContext";
<<<<<<< HEAD
import { isValidEmail, sanitizeInput, rateLimiter, secureFetch } from "@/lib/security";
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e

export function useLoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
<<<<<<< HEAD
  const [errors, setErrors] = useState<{email?: string; password?: string; general?: string}>({});

  const { login } = useAuth();
  const router = useRouter();

  const validateForm = (): boolean => {
    const newErrors: {email?: string; password?: string; general?: string} = {};

    // Sanitize inputs
    const sanitizedEmail = sanitizeInput(email, 254);
    const sanitizedPassword = sanitizeInput(password, 128);

    // Validate email
    if (!sanitizedEmail) {
      newErrors.email = "Email jest wymagany";
    } else if (!isValidEmail(sanitizedEmail)) {
      newErrors.email = "Nieprawidłowy format email";
    }

    // Validate password
    if (!sanitizedPassword) {
      newErrors.password = "Hasło jest wymagane";
    } else if (sanitizedPassword.length < 8) {
      newErrors.password = "Hasło musi mieć co najmniej 8 znaków";
    }

    // Rate limiting
    if (!rateLimiter.isAllowed(`login_${sanitizedEmail}`, 5, 15 * 60 * 1000)) { // 5 attempts per 15 minutes
      newErrors.general = "Zbyt wiele prób logowania. Spróbuj ponownie później.";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setErrors({});

    if (!validateForm()) {
      setLoading(false);
      return;
    }

    const sanitizedEmail = sanitizeInput(email, 254);
    const sanitizedPassword = sanitizeInput(password, 128);

    try {
      // 1. Pobranie użytkowników z bezpiecznym fetch
      const usersRes = await secureFetch(`/api/user`);
      if (!usersRes.ok) throw new Error("Błąd pobierania użytkowników");

      const users = await usersRes.json();
      const foundUser = users.find((u: any) => u.email === sanitizedEmail);
      if (!foundUser) {
        rateLimiter.reset(`login_${sanitizedEmail}`); // Reset on failed attempt
        throw new Error("Nie znaleziono użytkownika");
      }

      // 2. Logowanie z bezpiecznym fetch
      const loginRes = await secureFetch(`/api/user/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id: foundUser.id, password: sanitizedPassword }),
      });

      if (!loginRes.ok) {
        rateLimiter.reset(`login_${sanitizedEmail}`); // Reset on failed attempt
        throw new Error("Błędne dane logowania");
      }

      const data = await loginRes.json();

      // Secure login using the auth context
      login(data);
      localStorage.setItem("user", JSON.stringify(data));

      // Set secure cookie
      document.cookie = `user_role=${data.role}; path=/; max-age=36000; secure; samesite=strict`; // 10 hours, secure

      // Przekierowanie
=======

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
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
      if (data.role?.toLowerCase() === "admin") {
        router.push("/admin/tickets");
      } else {
        router.push("/");
      }
    } catch (error: any) {
<<<<<<< HEAD
      setErrors({ general: error.message || "Błąd logowania" });
=======
      alert(error.message || "Błąd logowania");
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
    } finally {
      setLoading(false);
    }
  };

<<<<<<< HEAD
  return {
    email,
    password,
    loading,
    errors,
    setEmail: (value: string) => {
      setEmail(value);
      if (errors.email) setErrors({ ...errors, email: undefined });
    },
    setPassword: (value: string) => {
      setPassword(value);
      if (errors.password) setErrors({ ...errors, password: undefined });
    },
    handleSubmit
  };
=======
  return { email, password, loading, setEmail, setPassword, handleSubmit };
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
}

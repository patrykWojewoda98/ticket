"use client";

import { useState } from "react";

export function useRegisterForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);

  const resetForm = () => {
    setEmail("");
    setPassword("");
    setConfirmPassword("");
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (password.length < 8) {
      alert("Hasło musi mieć min. 8 znaków");
      return;
    }

    if (password !== confirmPassword) {
      alert("Hasła nie są takie same");
      return;
    }

    setLoading(true);

    await new Promise((res) => setTimeout(res, 1200));

    console.log({ email, password });

    resetForm();
    setLoading(false);
  };

  return {
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
  };
}
"use client";

import { useState } from "react";

export function useLoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const resetForm = () => {
    setEmail("");
    setPassword("");
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    setLoading(true);

    await new Promise((res) => setTimeout(res, 1000)); // mock API

    console.log({ email, password });
    alert("Logowanie (mock)");

    resetForm();
    setLoading(false);
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
"use client";

import { useState } from "react";

export function useNewTicketForm() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [status, setStatus] = useState("OPEN");

  const resetForm = () => {
    setTitle("");
    setDescription("");
    setStatus("OPEN");
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    console.log({ title, description, status });
    alert("Ticket został utworzony (mock)");

    resetForm();
  };

  return {
    title,
    description,
    status,
    setTitle,
    setDescription,
    setStatus,
    handleSubmit,
  };
}
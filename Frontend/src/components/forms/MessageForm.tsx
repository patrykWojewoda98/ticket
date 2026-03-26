"use client";

import { useState } from "react";

export default function MessageForm({
  userId,
  ticketId,
  onSuccess,
}: {
  userId: number;
  ticketId: number;
  onSuccess?: () => void;
}) {
  const [content, setContent] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const res = await fetch(
      `${process.env.NEXT_PUBLIC_APP_URL}/api/comment`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          userId,
          ticketId, 
          content,
        }),
      }
    );

    if (!res.ok) {
      console.error(await res.text());
      return;
    }

    setContent("");
    onSuccess?.();
  };

  return (
    <form onSubmit={handleSubmit} className="flex gap-2 mt-2">
      <input
        value={content}
        onChange={(e) => setContent(e.target.value)}
        placeholder="Napisz wiadomość..."
        className="flex-1 border px-3 py-2 rounded"
        required
      />

      <button className="bg-blue-500 text-white px-4 rounded">
        Wyślij
      </button>
    </form>
  );
}
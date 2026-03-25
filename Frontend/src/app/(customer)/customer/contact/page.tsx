"use client";

import { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";

type Ticket = {
  id: number;
  title?: string;
};

type Message = {
  id: number;
  userId: number;
  ticketId: number;
  content: string;
  createdAt: string;
};

export default function Home() {
  const currentUserId = 6; // 🔥 USER

  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [selectedTicketId, setSelectedTicketId] = useState<number | null>(null);
  const [messages, setMessages] = useState<Message[]>([]);
  const [content, setContent] = useState("");

  const safeFetchJson = async (url: string) => {
    const res = await fetch(url);

    if (!res.ok) {
      console.error(await res.text());
      throw new Error("API error");
    }

    return res.json();
  };

  // 📥 TYLKO TICKETY USERA
  useEffect(() => {
    safeFetchJson(
      `${process.env.NEXT_PUBLIC_APP_URL}/api/ticket/user/${currentUserId}`
    )
      .then(setTickets)
      .catch(console.error);
  }, []);

  // 📥 WIADOMOŚCI DLA TICKETA
  const fetchMessages = async (ticketId: number) => {
    const data = await safeFetchJson(
      `${process.env.NEXT_PUBLIC_APP_URL}/api/comment/ticket/${ticketId}`
    );

    setMessages(data);
  };

  const handleSelectTicket = (ticketId: number) => {
    setSelectedTicketId(ticketId);
    fetchMessages(ticketId);
  };

  // 📤 WYŚLIJ WIADOMOŚĆ
  const handleSend = async () => {
    if (!selectedTicketId || !content) return;

    const res = await fetch(
      `${process.env.NEXT_PUBLIC_APP_URL}/api/comment`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          userId: currentUserId,
          ticketId: selectedTicketId,
          content,
        }),
      }
    );

    if (!res.ok) {
      console.error(await res.text());
      return;
    }

    setContent("");
    fetchMessages(selectedTicketId);
  };

  return (
    <div className="flex flex-col h-screen bg-gray-100 p-4">
      <h1 className="text-2xl font-bold mb-4">Moje zgłoszenia</h1>

      {/* 🎟️ TYLKO MOJE TICKETY */}
      <select
        className="border p-2 mb-4 w-full"
        onChange={(e) => handleSelectTicket(Number(e.target.value))}
        defaultValue=""
      >
        <option value="">Wybierz zgłoszenie</option>

        {tickets.map((t) => (
          <option key={t.id} value={t.id}>
            Ticket #{t.id}
          </option>
        ))}
      </select>

      {/* 💬 CHAT */}
      <div className="flex-1 overflow-y-auto space-y-2 p-2 border rounded-lg bg-white">
        {messages.map((msg) => {
          const isMine = msg.userId === currentUserId;

          return (
            <div
              key={msg.id}
              className={`flex ${
                isMine ? "justify-end" : "justify-start"
              }`}
            >
              <div
                className={`max-w-xs px-4 py-2 rounded-lg break-words ${
                  isMine
                    ? "bg-blue-500 text-white rounded-br-none"
                    : "bg-gray-300 text-gray-900 rounded-bl-none"
                }`}
              >
                {msg.content}

                <div className="text-xs text-gray-600 mt-1">
                  {msg.createdAt}
                </div>
              </div>
            </div>
          );
        })}
      </div>

      {/* ✉️ INPUT */}
      <div className="mt-4 flex gap-2">
        <input
          type="text"
          value={content}
          onChange={(e) => setContent(e.target.value)}
          placeholder="Napisz wiadomość..."
          className="flex-1 px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
        />

        <Button onClick={handleSend}>Wyślij</Button>
      </div>
    </div>
  );
}
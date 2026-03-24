"use client";

import { useEffect, useState } from "react";
import MessageForm from "@/components/forms/MessageForm";

type User = {
  id: number;
  name: string;
  email: string;
  role: "Admin" | "User";
};

type Ticket = {
  id: number;
};

type Message = {
  id: number;
  userId: number;
  ticketId: number;
  content: string;
};

export default function ContactPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [selectedTicketId, setSelectedTicketId] = useState<number | null>(null);
  const [messages, setMessages] = useState<Message[]>([]);

  const currentUserId = 7; // 🔥 ADMIN ID

  // ✅ FETCH JSON
  const safeFetchJson = async (url: string) => {
    const res = await fetch(url);

    if (!res.ok) {
      console.error(await res.text());
      throw new Error("API error");
    }

    return res.json();
  };

  // 📥 USERS (tylko klienci)
  useEffect(() => {
    safeFetchJson(`${process.env.NEXT_PUBLIC_APP_URL}/api/user`)
      .then((data) => {
        const onlyUsers = data.filter((u: User) => u.role === "User");
        setUsers(onlyUsers);
      })
      .catch(console.error);
  }, []);

  // 📥 TICKETS
  useEffect(() => {
    safeFetchJson(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket`)
      .then(setTickets)
      .catch(console.error);
  }, []);

  // 📥 MESSAGES PO TICKECIE
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

  return (
    <div className="flex flex-col h-screen p-4 bg-gray-100">
      <h1 className="text-2xl font-bold mb-4">Chat (Ticket)</h1>

      {/* 🎟️ SELECT TICKET */}
      <select
        className="border p-2 mb-4 w-full"
        onChange={(e) => handleSelectTicket(Number(e.target.value))}
        defaultValue=""
      >
        <option value="">Wybierz ticket</option>

        {tickets.map((t) => (
          <option key={t.id} value={t.id}>
            Ticket #{t.id}
          </option>
        ))}
      </select>

      {/* 💬 CHAT */}
      <div className="flex-1 overflow-y-auto bg-white p-3 border rounded">
        {messages.map((msg) => {
          const isMine = msg.userId === currentUserId;

          return (
            <div
              key={msg.id}
              className={`flex ${isMine ? "justify-end" : "justify-start"}`}
            >
              <div
                className={`px-3 py-2 rounded-lg max-w-xs ${
                  isMine
                    ? "bg-blue-500 text-white"
                    : "bg-gray-300 text-black"
                }`}
              >
                {msg.content}
              </div>
            </div>
          );
        })}
      </div>

      {/* ✉️ FORM */}
      {selectedTicketId && (
        <MessageForm
          userId={currentUserId}
          ticketId={selectedTicketId}
          onSuccess={() => fetchMessages(selectedTicketId)}
        />
      )}
    </div>
  );
}
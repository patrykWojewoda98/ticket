import { mockTickets } from "@/lib/mock-tickets";
import { mockMessages } from "@/lib/mock-messages";
import { notFound } from "next/navigation";

type Props = { params: { id: string } | Promise<{ id: string }> };

export default async function TicketDetailPage({ params }: Props) {
  // jeśli params jest Promise, odczekujemy
  const { id } = await params;
  const ticketId = Number(id);

  const ticket = mockTickets.find((t) => t.id === ticketId);
  if (!ticket) return notFound();

  const messages = mockMessages[ticket.id] || [];

  return (
    <div className="flex flex-col gap-6 mt-16">
      
      <h1 className="text-2xl font-bold text-slate-900">{ticket.title}</h1>

     
      <div className="flex flex-col gap-2 bg-slate-100 p-4 rounded-lg">
        <div><span className="font-semibold">ID:</span> {ticket.id}</div>
        <div className="flex items-center gap-2">
          <span className="font-semibold">Status:</span>
          <span className={`px-2 py-1 rounded-full text-sm status-${ticket.status.toLowerCase()}`}>
            {ticket.status}
          </span>
        </div>
        <div><span className="font-semibold">Utworzono:</span> {ticket.createdAt}</div>
      </div>

     
      <div className="flex flex-col gap-4">
        {messages.map((msg) => (
          <div
            key={msg.id}
            className={`max-w-xs p-3 rounded-lg ${
              msg.author === "CUSTOMER"
                ? "self-end bg-blue-100 text-slate-900"
                : "self-start bg-slate-200 text-slate-900"
            }`}
          >
            <div className="text-sm">{msg.content}</div>
            <div className="text-xs text-slate-500 mt-1">{msg.createdAt}</div>
          </div>
        ))}
      </div>

      
      <div className="mt-6">
        <input
          type="text"
          placeholder="Napisz wiadomość..."
          className="w-full border rounded-lg p-2 text-sm"
        />
        <button className="mt-2 px-4 py-2 bg-blue-600 text-white rounded-lg">
          Wyślij
        </button>
      </div>
    </div>
  );
}
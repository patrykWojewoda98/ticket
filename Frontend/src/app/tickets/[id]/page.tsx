import { mockTickets } from "@/lib/mock-tickets";
import { mockMessages } from "@/lib/mock-messages";
import { notFound } from "next/navigation";

type Props = { params: { id: string } };

export default function TicketDetailPage({ params }: Props) {
  const ticket = mockTickets.find((t) => t.id === params.id);
  if (!ticket) return notFound();

  const messages = mockMessages[ticket.id] || [];

  return (
    <div className="flex flex-col gap-6 mt-16">
      <h1 className="text-2xl font-bold text-slate-900">{ticket.title}</h1>
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
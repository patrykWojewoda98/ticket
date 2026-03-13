"use client";

import { Button } from "@/components/ui/button";
import { useNewTicketForm } from "@/hooks/useNewTicketForm";

export default function NewTicketPage() {
  const {
    title,
    description,
    status,
    setTitle,
    setDescription,
    setStatus,
    handleSubmit,
  } = useNewTicketForm();

  return (
    <div className="max-w-xl mx-auto flex flex-col gap-6">
      <h1 className="text-2xl font-bold text-slate-900">
        Utwórz nowy ticket
      </h1>

      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">Tytuł</label>
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Krótki opis problemu"
            className="border rounded-lg p-2 text-sm"
            required
          />
        </div>

        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">Opis</label>
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Opisz problem szczegółowo"
            className="border rounded-lg p-2 text-sm resize-none h-32"
            required
          />
        </div>

        <div className="flex flex-col gap-1">
          <label className="font-medium text-slate-700">
            Status (na razie tylko mock)
          </label>
          <select
            value={status}
            onChange={(e) => setStatus(e.target.value)}
            className="border rounded-lg p-2 text-sm"
          >
            <option value="OPEN">OPEN</option>
            <option value="PENDING">PENDING</option>
            <option value="CLOSED">CLOSED</option>
          </select>
        </div>

        <Button
          type="submit"
          className="transition-all duration-200 hover:scale-105 active:scale-95"
        >
          Utwórz ticket
        </Button>
      </form>
    </div>
  );
}
"use client";

import { useEffect, useState } from "react";
import Link from "next/link";

type Ticket = {
  id: number;
  priorityId: number;
  title: string;
  userId: number;
  assigneeId: number;
  categoryId: number;
  description: string;
  createdAt?: string;
  statusId: number;
};

export default function TicketsPage() {
  const [tickets, setTickets] = useState<Ticket[]>([]);

  useEffect(() => {
    fetchTickets();
  }, []);

  const fetchTickets = async () => {
    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_APP_URL}/api/ticket`
      );

      if (!res.ok) {
        console.error("API error:", res.status);
        return;
      }

      const text = await res.text();
      const data = text ? JSON.parse(text) : [];

      setTickets(data);
    } catch (error) {
      console.error("Error fetching tickets:", error);
    }
  };

  const getStatusLabel = (statusId: number) => {
    switch (statusId) {
      case 1:
        return { label: "OPEN", color: "text-green-600" };
      case 2:
        return { label: "PENDING", color: "text-yellow-600" };
      case 3:
        return { label: "CLOSED", color: "text-red-600" };
      default:
        return { label: "UNKNOWN", color: "text-gray-600" };
    }
  };

  const getPriorityLabel = (priorityId: number) => {
    switch (priorityId) {
      case 1:
        return {
          label: "LOW",
          color: "bg-blue-100 text-blue-700 border-blue-200",
        };
      case 2:
        return {
          label: "MEDIUM",
          color: "bg-yellow-100 text-yellow-700 border-yellow-200",
        };
      case 3:
        return {
          label: "HIGH",
          color: "bg-red-100 text-red-700 border-red-200",
        };
      default:
        return {
          label: "UNKNOWN",
          color: "bg-gray-100 text-gray-700 border-gray-200",
        };
    }
  };

  return (
    <div className="flex flex-col gap-8 mt-16">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold text-slate-900">
          Moje tickety
        </h1>
      </div>

      <div className="overflow-hidden rounded-2xl border shadow-sm">
        <table className="w-full">
          <thead>
            <tr className="bg-gray-100 text-left text-sm">
              <th className="p-3">Title</th>
              <th className="p-3">Description</th>
              <th className="p-3">Status</th>
              <th className="p-3">Priority</th>
              <th className="p-3">Action</th>
            </tr>
          </thead>

          <tbody>
            {tickets.map((ticket) => {
              const status = getStatusLabel(ticket.statusId);
              const priority = getPriorityLabel(ticket.priorityId);

              return (
                <tr
                  key={ticket.id}
                  className="border-t hover:bg-gray-50"
                >
                  <td className="p-3 font-medium">
                    {ticket.title}
                  </td>

                  <td className="p-3 text-gray-600">
                    {ticket.description}
                  </td>

                  <td className={`p-3 font-semibold ${status.color}`}>
                    {status.label}
                  </td>

                  <td className="p-3">
                    <span
                      className={`inline-flex items-center justify-center w-24 px-2 py-1 rounded-full text-xs font-semibold border ${priority.color}`}
                    >
                      {priority.label}
                    </span>
                  </td>

                  <td className="p-3">
                    <Link
                      href={`/customer/tickets/${ticket.id}`}
                      className="text-blue-600 hover:underline text-sm"
                    >
                      View
                    </Link>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}
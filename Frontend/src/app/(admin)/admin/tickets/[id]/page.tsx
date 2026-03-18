// ================= EDIT CLIENT PAGE =================
"use client";

import { useState } from "react";
import { useRouter, useParams } from "next/navigation";
import { mockUsers } from "@/lib/mock-client";

export function EditClientPage() {
  const router = useRouter();
  const params = useParams();
  const userIndex = Number(params.id);

  const [users, setUsers] = useState(mockUsers);
  const user = users[userIndex];

  const [formData, setFormData] = useState({
    companyName: user?.companyName || "",
    email: user?.email || "",
    emailVerified: user?.emailVerified || false,
    role: user?.role || "user",
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const target = e.target;

    if (target instanceof HTMLInputElement && target.type === "checkbox") {
      setFormData((prev) => ({
        ...prev,
        [target.name]: target.checked,
      }));
    } else {
      setFormData((prev) => ({
        ...prev,
        [target.name]: target.value,
      }));
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const updatedUsers = [...users];
    updatedUsers[userIndex] = {
      ...updatedUsers[userIndex],
      ...formData,
    };

    setUsers(updatedUsers);
    router.push("/admin/clients");
  };

  if (!user) return <div className="mt-16">User not found</div>;

  return (
    <div className="mt-16 max-w-2xl">
      <h1 className="text-2xl font-bold mb-6">Edit Client</h1>

      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input
          type="text"
          name="companyName"
          value={formData.companyName}
          onChange={handleChange}
          placeholder="Company Name"
          className="w-full border p-2 rounded"
        />

        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          placeholder="Email"
          className="w-full border p-2 rounded"
        />

        <label className="flex items-center gap-2 text-sm">
          <input
            type="checkbox"
            name="emailVerified"
            checked={formData.emailVerified}
            onChange={handleChange}
          />
          Email verified
        </label>

        <select
          name="role"
          value={formData.role}
          onChange={handleChange}
          className="w-full border p-2 rounded"
        >
          <option value="user">User</option>
          <option value="admin">Admin</option>
        </select>

        <div className="flex gap-3">
          <button className="bg-blue-600 text-white px-4 py-2 rounded">
            Save
          </button>
          <button
            type="button"
            onClick={() => router.push("/admin/clients")}
            className="border px-4 py-2 rounded"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
}

// ================= ADMIN TICKET DETAILS =================

export default function AdminTicketDetails({
  params,
}: {
  params: { id: string };
}) {
  const ticket = {
    title: "Printer not working",
    description: "The office printer is not responding.",
    company: "Acme Corp",
    priority: "high",
    status: "open",
  };

  return (
    <div className="mt-16 max-w-3xl">
      <h1 className="text-2xl font-bold mb-6">
        Ticket Details
      </h1>

      <div className="rounded-2xl border shadow-sm p-6 space-y-6">
        <div>
          <p className="text-sm text-gray-500">Title</p>
          <p className="text-lg font-semibold">{ticket.title}</p>
        </div>

        <div>
          <p className="text-sm text-gray-500">Company</p>
          <p className="font-medium">{ticket.company}</p>
        </div>

        <div>
          <p className="text-sm text-gray-500">Description</p>
          <p className="mt-1 text-gray-700">{ticket.description}</p>
        </div>

        <div className="flex gap-4">
          <div>
            <p className="text-sm text-gray-500">Status</p>
            <span className="px-3 py-1 rounded-full text-sm bg-blue-100 text-blue-700">
              {ticket.status}
            </span>
          </div>

          <div>
            <p className="text-sm text-gray-500">Priority</p>
            <span className="px-3 py-1 rounded-full text-sm bg-red-100 text-red-700">
              {ticket.priority}
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}

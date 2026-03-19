import Link from "next/link";
import React from "react";

export interface User {
  id: number;
  companyId: number;
  name: string;
  email: string;
  emailVerified: boolean;
  role: string;
}

interface ClientsTableProps {
  users: User[];
  onDelete: (id: number) => void;
}

export default function ClientsTable({ users, onDelete }: ClientsTableProps) {
  return (
    <div className="overflow-auto rounded-2xl border shadow-sm">
      <table className="w-full min-w-[900px]">
        <thead>
          <tr className="bg-gray-100 text-left text-sm">
            <th className="p-3">ID</th>
            <th className="p-3">Name</th>
            <th className="p-3">Company ID</th>
            <th className="p-3">Email</th>
            <th className="p-3">Email Verified</th>
            <th className="p-3">Role</th>
            <th className="p-3">Action</th>
          </tr>
        </thead>

        <tbody>
          {users.map((user) => (
            <tr key={user.id} className="border-t hover:bg-gray-50 transition">
              <td className="p-3">{user.id}</td>
              <td className="p-3 font-medium">{user.name || "N/A"}</td>
              <td className="p-3">{user.companyId}</td>
              <td className="p-3 text-gray-600">{user.email}</td>
              <td className="p-3">
                <span
                  className={`px-2 py-1 rounded-full text-xs font-medium ${
                    user.emailVerified
                      ? "bg-green-100 text-green-700"
                      : "bg-gray-200 text-gray-600"
                  }`}
                >
                  {user.emailVerified ? "Verified" : "Not verified"}
                </span>
              </td>
              <td className="p-3 capitalize">{user.role}</td>
              <td className="p-3">
                <div className="flex gap-2">
                  <Link
                    href={`/admin/clients/${user.id}/edit`}
                    className="px-3 py-1.5 text-sm rounded-lg bg-blue-600 text-white hover:bg-blue-700 transition"
                  >
                    Edit
                  </Link>

                  <button
                    onClick={() => onDelete(user.id)}
                    className="px-3 py-1.5 text-sm rounded-lg border border-red-300 text-red-600 hover:bg-red-50 transition"
                  >
                    Delete
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
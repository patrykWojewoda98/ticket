import Link from "next/link";
import { mockUsers } from "@/lib/mock-client";

export default function ClientsPage() {

  return (
    <div>

      <div className="flex justify-between mb-6">
        <h1 className="text-2xl font-bold">
          Clients
        </h1>

        <Link
          href="/admin/clients/new"
          className="bg-black text-white px-4 py-2"
        >
          New Client
        </Link>
      </div>

      <table className="w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="p-3">Name</th>
            <th className="p-3">Email</th>
            <th className="p-3">Email Verified</th>
            <th className="p-3">Role</th>
            <th className="p-3">Action</th>
          </tr>
        </thead>

        <tbody>
          {mockUsers.map((user, index) => (
            <tr key={index} className="border-t">
              <td className="p-3">{user.companyName || "N/A"}</td>
              <td className="p-3">{user.email}</td>
              <td className="p-3">{user.emailVerified ? "Yes" : "No"}</td>
              <td className="p-3">{user.role}</td>
              <td className="p-3 flex gap-3">
                <Link href={`/admin/clients/${index}/edit`}>
                  Edit
                </Link>
                <button>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

    </div>
  );
}
import Link from "next/link";
import { mockUsers } from "@/lib/mock-client";

export default function ClientsPage() {
  return (
    <div className="mt-16">
      <div className="flex justify-between mb-6">
        <h1 className="text-2xl font-bold">Clients</h1>
      </div>

      <div className="overflow-hidden rounded-2xl border shadow-sm">
        <table className="w-full">
          <thead>
            <tr className="bg-gray-100 text-left text-sm">
              <th className="p-3">Name</th>
              <th className="p-3">Email</th>
              <th className="p-3">Email Verified</th>
              <th className="p-3">Role</th>
              <th className="p-3">Action</th>
            </tr>
          </thead>

          <tbody>
            {mockUsers.map((user, index) => (
              <tr
                key={index}
                className="border-t hover:bg-gray-50 transition"
              >
                <td className="p-3 font-medium">
                  {user.companyName || "N/A"}
                </td>
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
                      href={`/admin/clients/${index}/edit`}
                      className="px-3 py-1.5 text-sm rounded-lg bg-blue-600 text-white hover:bg-blue-700 transition"
                    >
                      Edit
                    </Link>

                    <button
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
    </div>
  );
}
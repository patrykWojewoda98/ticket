"use client";

import { useState } from "react";
import { useRouter, useParams } from "next/navigation";
import { mockUsers } from "@/lib/mock-client";

export default function EditClientPage() {
  const router = useRouter();
  const params = useParams();
  const userIndex = Number(params.id);

  // lokalna kopia mocków (symulacja "bazy")
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

    // symulacja update w mock data
    const updatedUsers = [...users];
    updatedUsers[userIndex] = {
      ...updatedUsers[userIndex],
      ...formData,
    };

    setUsers(updatedUsers);

    console.log("Updated user (mock):", updatedUsers[userIndex]);

    router.push("/admin/clients");
  };

  if (!user) {
    return <div className="mt-16">User not found</div>;
  }

  return (
    <div className="mt-16 max-w-2xl">
      <h1 className="text-2xl font-bold mb-6">Edit Client</h1>

      <form onSubmit={handleSubmit} className="flex flex-col gap-4">

        <div>
          <label className="block mb-1 font-medium">Company Name</label>
          <input
            type="text"
            name="companyName"
            value={formData.companyName}
            onChange={handleChange}
            className="w-full border p-2 rounded"
          />
        </div>

        <div>
          <label className="block mb-1 font-medium">Email</label>
          <input
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            className="w-full border p-2 rounded"
          />
        </div>

        <div className="flex items-center gap-2">
          <input
            type="checkbox"
            name="emailVerified"
            checked={formData.emailVerified}
            onChange={handleChange}
          />
          <label>Email Verified</label>
        </div>

        <div>
          <label className="block mb-1 font-medium">Role</label>
          <select
            name="role"
            value={formData.role}
            onChange={handleChange}
            className="w-full border p-2 rounded"
          >
            <option value="user">User</option>
            <option value="admin">Admin</option>
          </select>
        </div>

        <div className="flex gap-3 mt-4">
          <button
            type="submit"
            className="bg-blue-600 text-white px-4 py-2 rounded"
          >
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
"use client";

import React from "react";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

interface Company {
  id: number;
  name: string;
}

interface EditClientProps {
  formData: {
    companyId: number;
    name: string;
    email: string;
    role: string;
  };
  companies: Company[];
  onChange: (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => void;
  onRoleChange: (role: string) => void;
  onSubmit: (e: React.FormEvent) => void;
  onCancel: () => void;
}

export function EditClient({
  formData,
  companies,
  onChange,
  onRoleChange,
  onSubmit,
  onCancel,
}: EditClientProps) {
  return (
    <form onSubmit={onSubmit} className="flex flex-col gap-4">
      {/* COMPANY SELECT */}
      <div>
        <label className="block mb-1 font-medium">Company</label>
        <select
          name="companyId"
          value={formData.companyId}
          onChange={onChange}
          className="w-full border p-2 rounded"
        >
          <option value={0}>Wybierz firmę</option>
          {companies.map((c) => (
            <option key={c.id} value={c.id}>
              {c.name}
            </option>
          ))}
        </select>
      </div>

      {/* NAME */}
      <div>
        <label className="block mb-1 font-medium">Name</label>
        <input
          type="text"
          name="name"
          value={formData.name}
          onChange={onChange}
          className="w-full border p-2 rounded"
        />
      </div>

      {/* EMAIL */}
      <div>
        <label className="block mb-1 font-medium">Email</label>
        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={onChange}
          className="w-full border p-2 rounded"
        />
      </div>

      {/* ROLE */}
      <div>
        <label className="block mb-1 font-medium">Role</label>
        <Select value={formData.role} onValueChange={onRoleChange}>
          <SelectTrigger className="px-2 py-1 rounded-lg text-sm border bg-gray-100 text-gray-700">
            <SelectValue />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="user">User</SelectItem>
            <SelectItem value="admin">Admin</SelectItem>
          </SelectContent>
        </Select>
      </div>

      {/* BUTTONS */}
      <div className="flex gap-3 mt-4">
        <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">
          Save
        </button>
        <button type="button" onClick={onCancel} className="border px-4 py-2 rounded">
          Cancel
        </button>
      </div>
    </form>
  );
}
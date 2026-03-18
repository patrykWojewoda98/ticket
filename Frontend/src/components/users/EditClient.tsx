"use client";

import React from "react";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";

interface EditClientProps {
  formData: {
    companyId: number;
    name: string;
    email: string;
    emailVerified: boolean;
    role: string;
  };
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onRoleChange: (role: string) => void;
  onSubmit: (e: React.FormEvent) => void;
  onCancel: () => void;
}

export function EditClient({ formData, onChange, onRoleChange, onSubmit, onCancel }: EditClientProps) {
  return (
    <form onSubmit={onSubmit} className="flex flex-col gap-4">
      <div>
        <label className="block mb-1 font-medium">Company ID</label>
        <input
          type="number"
          name="companyId"
          value={formData.companyId}
          onChange={onChange}
          className="w-full border p-2 rounded"
        />
      </div>

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

      <div className="flex items-center gap-2">
        <input
          type="checkbox"
          name="emailVerified"
          checked={formData.emailVerified}
          onChange={onChange}
        />
        <label>Email Verified</label>
      </div>

      <div>
        <label className="block mb-1 font-medium">Role</label>
        <Select value={formData.role} onValueChange={onRoleChange}>
          <SelectTrigger
            className={`px-2 py-1 rounded-lg text-sm border ${
              formData.role === "admin"
                ? "bg-blue-100 text-blue-700"
                : "bg-gray-100 text-gray-700"
            }`}
          >
            <SelectValue />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="user">User</SelectItem>
            <SelectItem value="admin">Admin</SelectItem>
          </SelectContent>
        </Select>
      </div>

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
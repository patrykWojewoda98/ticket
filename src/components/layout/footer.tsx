"use client";

import { Button } from "@/components/ui/button";
import { Facebook, Twitter, Instagram } from "lucide-react";

export default function Footer() {
  const socialLinks = [
    { icon: <Facebook className="w-5 h-5" />, label: "Facebook" },
    { icon: <Twitter className="w-5 h-5" />, label: "Twitter" },
    { icon: <Instagram className="w-5 h-5" />, label: "Instagram" },
  ];

  return (
    <footer className="border-t bg-white">
      <div className="container mx-auto flex flex-col md:flex-row items-center justify-between py-6 gap-4 px-6">
        
        {/* Copyright */}
        <div className="text-sm text-slate-500 text-center md:text-left">
          &copy; {new Date().getFullYear()} Wszelkie prawa zastrzeżone.
        </div>

        
        <div className="hidden md:block h-6 border-l border-slate-200"></div>

        {/* Social / Links */}
        <div className="flex flex-col md:flex-row items-center gap-4">
          {socialLinks.map((item) => (
            <Button
              key={item.label}
              variant="ghost"
              className="p-2 hover:bg-slate-100 rounded-full transition-colors"
              aria-label={item.label}
            >
              {item.icon}
            </Button>
          ))}
        </div>
      </div>
    </footer>
  );
}
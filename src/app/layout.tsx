import { ReactNode } from "react";
import { Poppins } from "next/font/google";
import Header from "@/frontend/components/layout/header";
import Footer from "@/frontend/components/layout/footer";
import "./globals.css";

const poppins = Poppins({
  subsets: ["latin", "latin-ext"],
  weight: ["400", "700"],
  display: "swap",
});

interface LayoutProps {
  children: ReactNode;
}

export default function Layout({ children }: LayoutProps) {
  return (
    <html lang="pl">
      <body className={poppins.className}>
        <div className="flex flex-col mx-auto px-[10%] 2xl:px-32 min-w-75 max-w-325 min-h-screen">
          <Header />

          <main className="flex-grow">{children}</main>

          <Footer />
        </div>
      </body>
    </html>
  );
}

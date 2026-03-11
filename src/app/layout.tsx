import { ReactNode } from "react";
import { Poppins } from "next/font/google";
import Header from "@/components/layout/header";
import Footer from "@/components/layout/footer";
import "./globals.css";
const poppins = Poppins({
  subsets: ["latin", "latin-ext"],
  weight: ["400", "700"],
  display: "swap",
});

interface LayoutProps {
  children: ReactNode;
}

export default async function Layout({ children }: LayoutProps) {
  // const session = await auth.api.getSession({ headers: await headers() });

  return (
    <html lang="pl">
      <body className={poppins.className}>
        <div className="mx-auto px-[10%] 2xl:px-32 min-w-75 max-w-325">
          <div className="gap-8 sm:gap-16 md:gap-24 lg:gap-32 grid">
            <Header />
            <main>{children}</main>
            <Footer></Footer>
          </div>
        </div>
      </body>
    </html>
  );
}

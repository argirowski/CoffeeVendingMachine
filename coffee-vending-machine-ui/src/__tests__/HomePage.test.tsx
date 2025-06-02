import React from "react";
import { render, screen } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import HomePage from "../components/HomePage";

describe("HomePage", () => {
  it("renders the main heading", () => {
    render(
      <MemoryRouter>
        <HomePage />
      </MemoryRouter>
    );
    expect(
      screen.getByRole("heading", {
        name: /welcome to the coffee vending app/i,
      })
    ).toBeInTheDocument();
  });

  it("renders the add coffee type link", () => {
    render(
      <MemoryRouter>
        <HomePage />
      </MemoryRouter>
    );
    const links = screen.getAllByRole("link", { name: /click here/i });
    expect(links[0]).toBeInTheDocument();
    expect(links[0]).toHaveAttribute("href", "/coffees/add");
  });

  it("renders the check coffee types link", () => {
    render(
      <MemoryRouter>
        <HomePage />
      </MemoryRouter>
    );
    const links = screen.getAllByRole("link", { name: /click here/i });
    expect(links[1]).toBeInTheDocument();
    expect(links[1]).toHaveAttribute("href", "/coffees");
  });
});

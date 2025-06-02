import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import ConfirmNavigationModal from "../components/modals/ConfirmNavigationModal";

describe("ConfirmNavigationModal", () => {
  const defaultProps = {
    show: true,
    onHide: jest.fn(),
    onConfirm: jest.fn(),
  };

  it("renders the modal with correct text", () => {
    render(<ConfirmNavigationModal {...defaultProps} />);
    expect(screen.getByText(/confirm navigation/i)).toBeInTheDocument();
    expect(
      screen.getByText(
        /do you really want to go back\? you will lose all of your changes\./i
      )
    ).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /yes/i })).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /no/i })).toBeInTheDocument();
  });

  it("calls onConfirm when Yes is clicked", () => {
    const onConfirm = jest.fn();
    render(<ConfirmNavigationModal {...defaultProps} onConfirm={onConfirm} />);
    fireEvent.click(screen.getByRole("button", { name: /yes/i }));
    expect(onConfirm).toHaveBeenCalled();
  });

  it("calls onHide when No is clicked or modal is closed", () => {
    const onHide = jest.fn();
    render(<ConfirmNavigationModal {...defaultProps} onHide={onHide} />);
    fireEvent.click(screen.getByRole("button", { name: /no/i }));
    expect(onHide).toHaveBeenCalled();
  });
});

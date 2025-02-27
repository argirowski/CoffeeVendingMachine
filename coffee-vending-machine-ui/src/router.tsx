import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import HomePage from "./components/HomePage";
import CoffeeTypesList from "./components/CoffeeTypesList";
import AddCoffeeType from "./components/AddCoffeeType";
import CoffeeTypeDetails from "./components/CoffeeTypeDetails";
import EditCoffeeType from "./components/EditCoffeeType";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "/", element: <HomePage /> },
      { path: "/coffees", element: <CoffeeTypesList /> },
      { path: "/coffees/add", element: <AddCoffeeType /> },
      { path: "/coffees/:id", element: <CoffeeTypeDetails /> },
      { path: "/coffees/:id/edit", element: <EditCoffeeType /> },
    ],
  },
]);

export default router;

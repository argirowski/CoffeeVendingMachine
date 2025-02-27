import React, { useEffect, useState } from "react";
import { CoffeeTypeDTO } from "../interfaces/interfaces";
import { Button, Col, Container, Row, Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "./loader/LoadingSpinner";
import ConfirmDeleteModal from "./modals/ConfirmDeleteModal";
import { deleteCoffeeType, fetchCoffeeTypes } from "../services/coffeeService";

const CoffeeTypesList: React.FC = () => {
  const [coffeeTypes, setCoffeeTypes] = useState<CoffeeTypeDTO[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [showDeleteModal, setShowDeleteModal] = useState<boolean>(false);
  const [selectedCoffeeType, setSelectedCoffeeType] = useState<string | null>(
    null
  );
  const navigate = useNavigate();

  useEffect(() => {
    loadCoffeeTypes();
  }, []);

  const loadCoffeeTypes = async () => {
    try {
      const data = await fetchCoffeeTypes();
      setCoffeeTypes(data);
      setLoading(false);
    } catch (error) {
      console.error("There was an error fetching the coffee types!", error);
      setLoading(false);
    }
  };

  const handleDelete = (id: string) => {
    setSelectedCoffeeType(id);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    if (selectedCoffeeType) {
      try {
        await deleteCoffeeType(selectedCoffeeType);
        setShowDeleteModal(false);
        loadCoffeeTypes();
      } catch (error) {
        console.error("There was an error deleting the coffee type!", error);
      }
    }
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <Container className="component-container">
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Coffee Type</th>
            <th>Coffee Ingredients</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {coffeeTypes.map((coffeeType) => (
            <tr key={coffeeType.id}>
              <td>{coffeeType.name}</td>
              <td>
                {`Milk: ${coffeeType.coffeeIngredient.dosesOfMilk}, Sugar: ${
                  coffeeType.coffeeIngredient.packsOfSugar
                }, Cinnamon: ${
                  coffeeType.coffeeIngredient.cinnamon ? "Yes" : "No"
                }, Stevia: ${
                  coffeeType.coffeeIngredient.stevia ? "Yes" : "No"
                }, Coconut Milk: ${
                  coffeeType.coffeeIngredient.coconutMilk ? "Yes" : "No"
                }`}
              </td>
              <td>
                <Button
                  variant="primary"
                  onClick={() => navigate(`/coffees/${coffeeType.id}`)}
                >
                  View
                </Button>
                <Button
                  variant="secondary"
                  onClick={() => navigate(`/coffees/${coffeeType.id}/edit`)}
                  className="ms-2"
                >
                  Edit
                </Button>
                <Button
                  variant="danger"
                  onClick={() => handleDelete(coffeeType.id)}
                  className="ms-2"
                >
                  Delete
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Row className="mt-3">
        <Col className="d-flex justify-content-start">
          <Button
            variant="outline-success"
            onClick={() => navigate("/coffees/add")}
            className="me-3"
            size="lg"
          >
            Add New Coffee
          </Button>
          <Button
            variant="outline-dark"
            onClick={() => navigate("/")}
            size="lg"
          >
            Go to Home Page
          </Button>
        </Col>
      </Row>

      {showDeleteModal && <LoadingSpinner />}

      <ConfirmDeleteModal
        show={showDeleteModal}
        onHide={() => setShowDeleteModal(false)}
        onConfirm={confirmDelete}
      />
    </Container>
  );
};

export default CoffeeTypesList;

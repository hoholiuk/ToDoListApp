import CategoryInput from "../../../../models/categoryInput";

export const getCategoriesQuery = () => {
    return `
        query {
          categoryModelQuery {
            categories {
              id
              name
            }
          }
        }
    `;
};

export const createCategoryQuery = (category: CategoryInput) => {
    return `
        mutation {
          categoryModelMutation {
            createCategory(category: {
                name: "${category.name}"
            }) {
              id
              name
            }
          }
        }
    `;
};

export const removeCategoryQuery = (id: number) => {
    return `
        mutation {
          categoryModelMutation {
            deleteCategory(id: ${id})
          }
        }
    `;
}

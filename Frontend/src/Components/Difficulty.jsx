// eslint-disable-next-line no-unused-vars
import React, { useEffect, useState } from 'react';
import axios from "axios";

const Difficulty = () => {

  const [difficulties, setDifficulties] = useState([])
  useEffect(() => {
    axios.get("https://localhost:7258/api/Difficulty")
      .then((result) => {
        setDifficulties(result.data)
      })
      .catch((err) => console.log("Error while fetching the data " + err))
  }, [])

  return (
    <>
      <div className='mt-4 mb-4 container-fluid'>
        <h2 className="text-uppercase text-center mb-5">Difficulties</h2>
        {
          difficulties.length <= 0 ? <p className='text Center'>No Records Found!</p> :
            (
              <table className="table table-striped table-bordered">
                <thead>
                  <tr>
                    <th scope="col" className='text-center'>Difficulty Id</th>
                    <th scope="col" className='text-center'>Difficulty Level</th>
                    <th scope="col" className='text-center'>Actions</th>
                  </tr>
                </thead>
                <tbody className="table-group-divider">
                  {
                    difficulties.map((difficulty, index) => {
                      return (
                        <tr key={index}>
                          <th scope="row" className='text-center'>{difficulty.id}</th>
                          <td className='text-center'>{difficulty.name}</td>
                          <td className='text-center'>
                            <button className='btn btn-warning'>Edit</button>
                          </td>
                        </tr>
                      )
                    })
                  }
                </tbody>
              </table>
            )
        }

      </div>
    </>
  );
}

export default Difficulty;

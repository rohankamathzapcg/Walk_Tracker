// eslint-disable-next-line no-unused-vars
import React from 'react'
import { useEffect, useState } from 'react'
import axios from 'axios'

const Walk = () => {
  const [walkDetails, setWalkDetails] = useState([]);
  useEffect(() => {
    axios.get("https://localhost:7258/api/Walk")
      .then((result) => {
        setWalkDetails(result.data)
      })
      .catch((err) => console.log("Error while fetching data " + err))
  }, [])

  return (
    <>
      <div className='mt-4 mb-4 container-fluid'>
        <h2 className="text-uppercase text-center mb-5">Walking Details</h2>
        {
          walkDetails.length <= 0 ? <p className='text-center'>No records found</p> :
            (
              <div className="table-responsive">
                <table className="table table-striped table-bordered">
                  <thead>
                    <tr>
                      <th scope="col" className='text-center'>Place Name</th>
                      <th scope="col" className='text-center'>Description</th>
                      <th scope="col" className='text-center'>Distance</th>
                      <th scope="col" className='text-center'>Image</th>
                      <th scope="col" className='text-center'>Region Code</th>
                      <th scope="col" className='text-center'>Region Name</th>
                      <th scope="col" className='text-center'>Difficulty Level</th>
                      <th scope="col" className='text-center'>Actions</th>
                    </tr>
                  </thead>
                  <tbody className="table-group-divider">
                    {walkDetails.map((walk, index) => {
                      return (
                        <tr key={index}>
                          <th className='text-center'>{walk.name}</th>
                          <td className='text-center'>{walk.description}</td>
                          <td className='text-center'>{walk.distance}</td>
                          <td className='text-center'>{walk.imageURL == "" ? walk.imageURL : "No Image"}</td>
                          <td className='text-center'>{walk.region.code}</td>
                          <td className='text-center'>{walk.region.name}</td>
                          <td className='text-center'>{walk.difficulty.name}</td>
                          <td className='text-center'>
                            <button className='btn btn-warning me-2'>Edit</button>
                            <button className='btn btn-danger'>Delete</button>
                          </td>
                        </tr>
                      )
                    })}
                  </tbody>
                </table>
              </div>

            )
        }

      </div>
    </>
  )
}

export default Walk

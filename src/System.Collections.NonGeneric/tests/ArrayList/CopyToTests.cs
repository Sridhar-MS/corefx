// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using Xunit;

namespace System.Collections.ArrayListTests
{
    public class CopyToTests
    {
        #region "Test Data - Keep the data close to tests so it can vary independently from other tests"

        static string[] strHeroes = 
        {
            "Aquaman",
            "Atom",
            "Batman",
            "Black Canary",
            "Captain America",
            "Captain Atom",
            "Catwoman",
            "Cyborg",
            "Flash",
            "Green Arrow",
            "Green Lantern",
            "Hawkman",
            null,
            "Ironman",
            "Nightwing",
            "Robin",
            "SpiderMan",
            "Steel",
            null,
            "Thor",
            "Wildcat",
            null
        };

        string[] strHeroesWithoutNulls =
            {
                "Aquaman",
                "Atom",
                "Batman",
                "Black Canary",
                "Captain America",
                "Captain Atom",
                "Catwoman",
                "Cyborg",
                "Flash",
                "Green Arrow",
                "Green Lantern",
                "Hawkman",
                "Huntress",
                "Ironman",
                "Nightwing",
                "Robin",
                "SpiderMan",
                "Steel",
                "Superman",
                "Thor",
                "Wildcat",
                "Wonder Woman",
            };

        #endregion

        /// <summary>
        /// <Given>
        /// An ArrayList with elements
        /// </Given>
        /// <When>
        /// Copying the ArrayList to an array with sufficient space using ArrayList.CopyTo(array)
        /// </When>
        /// <Then>
        /// The array must be populated with all the elements from ArrayList in the same order.
        /// </Then>        
        /// </summary>
        [Fact]
        public void TestCopyToBasic1()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            String[] arrCopy = null;

            //
            // []  CopyTo an array normal
            //
            // []  Normal Copy Test 1

            arrList = new ArrayList(strHeroes);
            arrCopy = new String[strHeroes.Length];
            arrList.CopyTo(arrCopy);

            for (int i = 0; i < arrCopy.Length; i++)
            {
                Assert.Equal(strHeroes[i], arrCopy[i]);
            }
        }


        /// <summary>        
        /// <Given>
        /// An empty ArrayList
        /// </Given>
        /// <When>
        /// Copying the ArrayList to an array using ArrayList.CopyTo(array)
        /// </When>
        /// <Then>
        /// The array must be must be unchanged.
        /// </Then>
        /// </summary>
        [Fact]
        public void TestCopyToBasic2()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            String[] arrCopy = null;

            //[]  Normal Copy Test 2 - copy 0 elements
            // Construct ArrayList.
            arrList = new ArrayList();
            arrList.Add(null);
            arrList.Add(arrList);
            arrList.Add(null);
            arrList.Remove(null);
            arrList.Remove(null);
            arrList.Remove(arrList);

            Assert.Equal(0, arrList.Count);

            arrCopy = new String[strHeroes.Length];

            // put some elements in arrCopy that should not be overriden
            for (int i = 0; i < strHeroes.Length; i++)
            {
                arrCopy[i] = strHeroes[i];
            }

            //copying 0 elements into arrCopy
            arrList.CopyTo(arrCopy);

            // check to make sure sentinals stay the same
            for (int i = 0; i < arrCopy.Length; i++)
            {
                Assert.Equal(strHeroes[i], arrCopy[i]);
            }
        }

        /// <summary>        
        /// <Given>
        /// An empty ArrayList
        /// </Given>
        /// <When>
        /// Copying the ArrayList to an empty array using ArrayList.CopyTo(array)
        /// </When>
        /// <Then>
        /// The array must be must remain empty.
        /// </Then>
        /// </summary>
        [Fact]
        public void TestCopyToBasic3()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            String[] arrCopy = null;

            //we'll make sure by copying only 0
            arrList = new ArrayList();
            arrCopy = new String[0];

            //copying 0 elements into arrCopy
            arrList.CopyTo(arrCopy);
            Assert.Equal(0, arrCopy.Length);
        }

        /// <summary>        
        /// <Given>
        /// An empty ArrayList
        /// </Given>
        /// <When>
        /// Copying the ArrayList to a null array using ArrayList.CopyTo(array)
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(array) must throw ArgumentNullException.
        /// </Then>
        /// </summary>
        [Fact]
        public void TestCopyToBasic4()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            String[] arrCopy = null;

            //[]  Copy so that exception should be thrown
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Construct ArrayList.
                arrList = new ArrayList();
                arrCopy = null;

                //copying 0 elements into arrCopy, into INVALID index of arrCopy
                arrList.CopyTo(arrCopy);
            });
        }


        /// <summary>
        /// <Given>
        /// An ArrayList with elements
        /// </Given>
        /// <When>
        /// Copying the ArrayList to an array starting at index 0 using ArrayList.CopyTo(array, index)
        /// </When>
        /// <Then>
        /// The array must be populated with all the elements from ArrayList in the same order starting from index 0.
        /// </Then>        
        /// </summary>
        /// 
        [Fact]
        public void TestArrayListWrappers1()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            String[] arrCopy = null;

            arrList = new ArrayList(strHeroes);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    (ArrayList)arrList.Clone(),
                                    (ArrayList)ArrayList.Adapter(arrList).Clone(),
                                    (ArrayList)arrList.GetRange(0, arrList.Count).Clone(),
                                    (ArrayList)ArrayList.Synchronized(arrList).Clone()};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                arrList = arrayListType;
                //
                // []  CopyTo an array normal
                //
                arrCopy = new String[strHeroes.Length];
                arrList.CopyTo(arrCopy, 0);

                for (int i = 0; i < arrCopy.Length; i++)
                {
                    Assert.Equal<string>(strHeroes[i], arrCopy[i]);
                }
            }
        }


        /// <summary>        
        /// <Given>
        /// An empty ArrayList
        /// </Given>
        /// <When>
        /// Copying the ArrayList to an array starting at a valid index using ArrayList.CopyTo(array, index)
        /// </When>
        /// <Then>
        /// The array must be must be unchanged.
        /// </Then>
        /// </summary>
        [Fact]
        public void TestArrayListWrappers2()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            String[] arrCopy = null;

            arrList = new ArrayList(strHeroes);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    (ArrayList)arrList.Clone(),
                                    (ArrayList)ArrayList.Adapter(arrList).Clone(),
                                    (ArrayList)arrList.GetRange(0, arrList.Count).Clone(),
                                    (ArrayList)ArrayList.Synchronized(arrList).Clone()};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                //[]  Normal Copy Test 2 - copy 0 elements
                arrList.Clear();
                arrList.Add(null);
                arrList.Add(arrList);
                arrList.Add(null);
                arrList.Remove(null);
                arrList.Remove(null);
                arrList.Remove(arrList);

                Assert.Equal(0, arrList.Count);

                arrCopy = new String[strHeroes.Length];
                // put some elements in arrCopy that should not be overriden
                for (int i = 0; i < strHeroes.Length; i++)
                {
                    arrCopy[i] = strHeroes[i];
                }

                //copying 0 elements into arrCopy
                arrList.CopyTo(arrCopy, 1);

                // check to make sure sentinals stay the same
                for (int i = 0; i < arrCopy.Length; i++)
                {
                    Assert.Equal<string>(strHeroes[i], arrCopy[i]);
                }

                //[]  Normal Copy Test 3 - copy 0 elements from the end
                arrList.Clear();
                Assert.Equal(0, arrList.Count);

                arrCopy = new String[strHeroes.Length];

                // put some elements in arrCopy that should not be overriden
                for (int i = 0; i < strHeroes.Length; i++)
                {
                    arrCopy[i] = strHeroes[i];
                }

                //copying 0 elements into arrCopy, into last valid index of arrCopy
                arrList.CopyTo(arrCopy, arrCopy.Length - 1);

                // check to make sure sentinals stay the same
                for (int i = 0; i < arrCopy.Length; i++)
                {
                    Assert.Equal<string>(strHeroes[i], arrCopy[i]);
                }
            }
        }

        /// <summary>        
        /// <Given>
        /// An ArrayList
        /// </Given>
        /// <When>
        /// Copying the ArrayList to an array starting at a negative index using ArrayList.CopyTo(array, index)
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(array, index) must throw  ArgumentOutOfRangeException
        /// </Then>
        /// </summary>
        [Fact]
        public void TestArrayListWrappers3()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            String[] arrCopy = null;

            arrList = new ArrayList(strHeroes);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    (ArrayList)arrList.Clone(),
                                    (ArrayList)ArrayList.Adapter(arrList).Clone(),
                                    (ArrayList)arrList.GetRange(0, arrList.Count).Clone(),
                                    (ArrayList)ArrayList.Synchronized(arrList).Clone()};

            foreach (ArrayList arrayListType in arrayListTypes)
            {


                //[]  Copy so that exception should be thrown
                arrList.Clear();
                arrCopy = new String[2];

                //copying 0 elements into arrCopy
                arrList.CopyTo(arrCopy, arrCopy.Length);

                // []  Copy so that exception should be thrown 2
                arrList.Clear();
                Assert.Equal(0, arrList.Count);

                arrCopy = new String[0];
                //copying 0 elements into arrCopy
                arrList.CopyTo(arrCopy, 0);

                // []  CopyTo with negative index
                Assert.Throws<ArgumentOutOfRangeException>(() => arrList.CopyTo(arrCopy, -1));
            }
        }

        /// <summary>        
        /// <Given>
        /// An ArrayList
        /// </Given>
        /// <When>
        /// Using ArrayList.CopyTo(array, index) the number of elements in the source ArrayList is greater than the available space from 'index' to the end of the destination 'array'.
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(array, index) must throw  ArgumentException
        /// </Then>
        /// </summary>
        [Fact]
        public void TestArrayListWrappers4()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;


            arrList = new ArrayList(strHeroes);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    (ArrayList)arrList.Clone(),
                                    (ArrayList)ArrayList.Adapter(arrList).Clone(),
                                    (ArrayList)arrList.GetRange(0, arrList.Count).Clone(),
                                    (ArrayList)ArrayList.Synchronized(arrList).Clone()};

            foreach (ArrayList arrayListType in arrayListTypes)
            {

                // []  CopyTo with array with index is not large enough
                Assert.Throws<ArgumentException>(() =>
                {
                    arrList.Clear();
                    for (int i = 0; i < 10; i++)
                        arrList.Add(i);

                    arrList.CopyTo(new Object[11], 2);
                });

                // []  CopyTo with null array
                Assert.Throws<ArgumentNullException>(() => arrList.CopyTo(null, 0));

                // []  CopyTo with multidimentional array
                Assert.Throws<ArgumentException>(() => arrList.CopyTo(new Object[10, 10], 1));
            }
        }

        /// <summary>        
        /// <Given>
        /// An ArrayList
        /// </Given>
        /// <When>
        /// Copying the ArrayList to a null array ArrayList.CopyTo(array, index)
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(array, index) must throw  ArgumentNullException
        /// </Then>
        /// </summary>
        [Fact]
        public void TestArrayListWrappers5()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;


            arrList = new ArrayList(strHeroes);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    (ArrayList)arrList.Clone(),
                                    (ArrayList)ArrayList.Adapter(arrList).Clone(),
                                    (ArrayList)arrList.GetRange(0, arrList.Count).Clone(),
                                    (ArrayList)ArrayList.Synchronized(arrList).Clone()};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                // []  CopyTo with null array
                Assert.Throws<ArgumentNullException>(() => arrList.CopyTo(null, 0));
            }
        }

        /// <summary>        
        /// <Given>
        /// An ArrayList
        /// </Given>
        /// <When>
        /// Copying the ArrayList to a multi-dimensional array ArrayList.CopyTo(array, index)
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(array, index) must throw  ArgumentException
        /// </Then>
        /// </summary>
        [Fact]
        public void TestArrayListWrappers6()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;


            arrList = new ArrayList(strHeroes);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    (ArrayList)arrList.Clone(),
                                    (ArrayList)ArrayList.Adapter(arrList).Clone(),
                                    (ArrayList)arrList.GetRange(0, arrList.Count).Clone(),
                                    (ArrayList)ArrayList.Synchronized(arrList).Clone()};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                // []  CopyTo with multidimentional array
                Assert.Throws<ArgumentException>(() => arrList.CopyTo(new Object[10, 10], 1));
            }
        }

        /// <summary>
        /// <Given>
        /// An ArrayList with elements
        /// </Given>
        /// <When>
        /// Copying the ArrayList to an array using ArrayList.CopyTo(index, array, arrayIndex, count) with valid 'index', 'arrayIndex' and 'count'
        /// </When>
        /// <Then>
        /// The array starting at 'arrayIndex' must be populated with the correct 'count' of elements from the ArrayList starting from 'index'.
        /// </Then>        
        /// </summary>
        /// 
        [Fact]
        public void TestCopyToWithCount1()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            string[] arrCopy = null;



            //
            // Construct array list.
            //
            arrList = new ArrayList();
            Assert.NotNull(arrList);

            // Add items to the lists.
            for (int ii = 0; ii < strHeroesWithoutNulls.Length; ++ii)
            {
                arrList.Add(strHeroesWithoutNulls[ii]);
            }

            // Verify items added to list.
            Assert.Equal(strHeroesWithoutNulls.Length, arrList.Count);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    arrList,
                                    ArrayList.Adapter(arrList),
                                    ArrayList.FixedSize(arrList),
                                    arrList.GetRange(0, arrList.Count),
                                    ArrayList.ReadOnly(arrList),
                                    ArrayList.Synchronized(arrList)};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                arrList = arrayListType;
                //
                // []  Use CopyTo copy range of items to array.
                //
                int start = 3;
                int count = 15;

                // Allocate sting array.
                arrCopy = new String[100];

                // Obtain string from ArrayList.
                arrList.CopyTo(start, arrCopy, start, count);

                // Verify the items in the array.
                for (int ii = start; ii < start + count; ++ii)
                {
                    Assert.Equal(0, ((String)arrList[ii]).CompareTo(arrCopy[ii]));
                }
            }
        }

        /// <summary>
        /// <Given>
        /// An ArrayList with elements
        /// </Given>
        /// <When>
        /// When copying the ArrayList to an array using ArrayList.CopyTo(index, array, arrayIndex, count) with 'count' greater than number of elements in Arraylist starting at 'index'
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(index, array, arrayIndex, count) must throw ArgumentException.
        /// </Then>        
        /// </summary>
        /// 
        [Fact]
        public void TestCopyToWithCount2()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            string[] arrCopy = null;



            //
            // Construct array list.
            //
            arrList = new ArrayList();
            Assert.NotNull(arrList);

            // Add items to the lists.
            for (int ii = 0; ii < strHeroesWithoutNulls.Length; ++ii)
            {
                arrList.Add(strHeroesWithoutNulls[ii]);
            }

            // Verify items added to list.
            Assert.Equal(strHeroesWithoutNulls.Length, arrList.Count);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    arrList,
                                    ArrayList.Adapter(arrList),
                                    ArrayList.FixedSize(arrList),
                                    arrList.GetRange(0, arrList.Count),
                                    ArrayList.ReadOnly(arrList),
                                    ArrayList.Synchronized(arrList)};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                //
                // []  Invalid Arguments
                //

                // 2nd throw ArgumentOutOfRangeException
                // rest throw ArgumentException 
                Assert.ThrowsAny<ArgumentException>(() => arrList.CopyTo(0, arrCopy, -100, 1000));



                // this is valid now
                arrCopy = new String[100];
                arrList.CopyTo(arrList.Count, arrCopy, 0, 0);

                Assert.Throws<ArgumentException>(() =>
                {
                    arrCopy = new String[100];
                    arrList.CopyTo(arrList.Count - 1, arrCopy, 0, 24);
                });


            }
        }


        /// <summary>
        /// <Given>
        /// Given an ArrayList with elements
        /// </Given>
        /// <When>
        /// When copying the ArrayList to a multi-dimensional array using ArrayList.CopyTo(index, array, arrayIndex, count)
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(index, array, arrayIndex, count) must throw ArgumentException.
        /// </Then>        
        /// </summary>
        ///
        [Fact]
        public void TestCopyToWithCount3()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;




            //
            // Construct array list.
            //
            arrList = new ArrayList();
            Assert.NotNull(arrList);

            // Add items to the lists.
            for (int ii = 0; ii < strHeroesWithoutNulls.Length; ++ii)
            {
                arrList.Add(strHeroesWithoutNulls[ii]);
            }

            // Verify items added to list.
            Assert.Equal(strHeroesWithoutNulls.Length, arrList.Count);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    arrList,
                                    ArrayList.Adapter(arrList),
                                    ArrayList.FixedSize(arrList),
                                    arrList.GetRange(0, arrList.Count),
                                    ArrayList.ReadOnly(arrList),
                                    ArrayList.Synchronized(arrList)};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                Assert.Throws<ArgumentException>(() => arrList.CopyTo(0, new Object[arrList.Count, arrList.Count], 0, arrList.Count));
                // same as above, some iteration throws different exceptions: ArgumentOutOfRangeException
                Assert.ThrowsAny<ArgumentException>(() => arrList.CopyTo(0, new Object[arrList.Count, arrList.Count], 0, -1));
            }
        }

        /// <summary>
        /// <Given>
        /// Given an ArrayList with elements
        /// </Given>
        /// <When>
        /// When copying the ArrayList to a null array using ArrayList.CopyTo(index, array, arrayIndex, count)
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(index, array, arrayIndex, count) must throw ArgumentNullException.
        /// </Then>        
        /// </summary>
        /// 
        [Fact]
        public void TestCopyToWithCount4()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;




            //
            // Construct array list.
            //
            arrList = new ArrayList();
            Assert.NotNull(arrList);

            // Add items to the lists.
            for (int ii = 0; ii < strHeroesWithoutNulls.Length; ++ii)
            {
                arrList.Add(strHeroesWithoutNulls[ii]);
            }

            // Verify items added to list.
            Assert.Equal(strHeroesWithoutNulls.Length, arrList.Count);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    arrList,
                                    ArrayList.Adapter(arrList),
                                    ArrayList.FixedSize(arrList),
                                    arrList.GetRange(0, arrList.Count),
                                    ArrayList.ReadOnly(arrList),
                                    ArrayList.Synchronized(arrList)};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                Assert.Throws<ArgumentNullException>(() => arrList.CopyTo(0, null, 3, 15));
            }
        }

        /// <summary>
        /// <Given>
        /// Given an ArrayList with elements
        /// </Given>
        /// <When>
        /// When copying the ArrayList to a array using ArrayList.CopyTo(index, array, arrayIndex, count) with negative 'index' or 'arrayIndex' or 'count'
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(index, array, arrayIndex, count) must throw ArgumentOutOfRangeException.
        /// </Then>        
        /// </summary>
        ///
        [Fact]
        public void TestCopyToWithCount5()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            string[] arrCopy = null;



            //
            // Construct array list.
            //
            arrList = new ArrayList();
            Assert.NotNull(arrList);

            // Add items to the lists.
            for (int ii = 0; ii < strHeroesWithoutNulls.Length; ++ii)
            {
                arrList.Add(strHeroesWithoutNulls[ii]);
            }

            // Verify items added to list.
            Assert.Equal(strHeroesWithoutNulls.Length, arrList.Count);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    arrList,
                                    ArrayList.Adapter(arrList),
                                    ArrayList.FixedSize(arrList),
                                    arrList.GetRange(0, arrList.Count),
                                    ArrayList.ReadOnly(arrList),
                                    ArrayList.Synchronized(arrList)};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                //
                // []  Invalid Arguments
                //

                // 2nd throw ArgumentOutOfRangeException
                // Allocate sting array.
                arrCopy = new String[100];
                Assert.Throws<ArgumentOutOfRangeException>(() => arrList.CopyTo(-1, arrCopy, 0, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => arrList.CopyTo(0, arrCopy, 0, -1));




            }
        }

        /// <summary>
        /// <Given>
        /// Given an ArrayList with elements
        /// </Given>
        /// <When>
        /// When copying the ArrayList to an array using ArrayList.CopyTo(index, array, arrayIndex, count) with 'count' greater than number of elements in the array starting at 'arrayIndex'
        /// </When>
        /// <Then>
        /// ArrayList.CopyTo(index, array, arrayIndex, count) must throw ArgumentException.
        /// </Then>        
        /// </summary>
        [Fact]
        public void TestCopyToWithCount6()
        {
            //--------------------------------------------------------------------------
            // Variable definitions.
            //--------------------------------------------------------------------------
            ArrayList arrList = null;
            string[] arrCopy = null;



            //
            // Construct array list.
            //
            arrList = new ArrayList();
            Assert.NotNull(arrList);

            // Add items to the lists.
            for (int ii = 0; ii < strHeroesWithoutNulls.Length; ++ii)
            {
                arrList.Add(strHeroesWithoutNulls[ii]);
            }

            // Verify items added to list.
            Assert.Equal(strHeroesWithoutNulls.Length, arrList.Count);

            //Adapter, GetRange, Synchronized, ReadOnly returns a slightly different version of 
            //BinarySearch, Following variable cotains each one of these types of array lists

            ArrayList[] arrayListTypes = {
                                    arrList,
                                    ArrayList.Adapter(arrList),
                                    ArrayList.FixedSize(arrList),
                                    arrList.GetRange(0, arrList.Count),
                                    ArrayList.ReadOnly(arrList),
                                    ArrayList.Synchronized(arrList)};

            foreach (ArrayList arrayListType in arrayListTypes)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    arrCopy = new String[1];
                    arrList.CopyTo(0, arrCopy, 3, 15);
                });
            }
        }
    }
}
